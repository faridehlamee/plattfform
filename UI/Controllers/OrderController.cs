using AutoMapper;
using Common;
using Common.AllEnum;
using Data.Contracts;
using Data.Contracts.Cart;
using Data.Contracts.Discount;
using Data.Contracts.Financial;
using Data.Contracts.OfferItem;
using Data.Contracts.Order;
using Data.Contracts.User;
using Data.Contracts.Wallet;
using Data.Contracts.WareHouse;
using Data.DTO.Product;
using Data.DTO.Sales;
using Data.DTO.User;
using Entites.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZarinPal.Class;
using static Common.AllEnum.Commons;

namespace UI.Controllers
{
    public class OrderController : Controller
    {
        private Payment _Payment;
        string authority;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IPriceCenterItemRepository _priceCenterItemRepository;
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IDiscountRepository  _discountRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IProductWareHouseRepository _productWareHouseRepository;
        private readonly IToastNotification _toastNotification;
        private readonly IIdentificationCodeRuleRepository _identificationCodeRuleRepository;
        private readonly IIdentificationCode_logRepository _identificationCode_LogRepository;

        public OrderController(IMapper Mapper, IUserRepository UserRepository,
            IPriceCenterItemRepository priceCenterItemRepository, ICartRepository cartRepository,
            IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository,
            IDiscountRepository  discountRepository, IAddressRepository addressRepository,
            IWalletRepository walletRepository, IProductWareHouseRepository productWareHouseRepository,
            IToastNotification toastNotification, IIdentificationCodeRuleRepository identificationCodeRuleRepository,
            IIdentificationCode_logRepository identificationCode_LogRepository)
        {
            _mapper = Mapper;
            _userRepository = UserRepository;
            _priceCenterItemRepository = priceCenterItemRepository;
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _discountRepository = discountRepository;
            _addressRepository = addressRepository;
            _walletRepository = walletRepository;
            _productWareHouseRepository = productWareHouseRepository;
            _toastNotification = toastNotification;
            _identificationCodeRuleRepository = identificationCodeRuleRepository;
            _identificationCode_LogRepository = identificationCode_LogRepository;
            var expose = new Expose();
            _Payment = expose.CreatePayment();
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
            var cart = await _cartRepository.GetCart();
            if (cart == null)
            {
                _toastNotification.AddErrorToastMessage("سبد خرید خالی می باشد");
                return RedirectToAction("Index", "Cart");
            }
            if (cart.Count == 0)
            {
                _toastNotification.AddErrorToastMessage("سبد خرید خالی می باشد");
                return RedirectToAction("Index", "Cart");
            }
            return View();
        }

        [Authorize(Roles = "Client")]
        public async Task<JsonResult> CreateOrder(CancellationToken cancellationToken)
        {
            var result = false;
            var data = new BillDTO();
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var cart = await _cartRepository.GetCart();
            if (cart == null)
                return Json(result);
            if (cart.Count == 0)
                return Json(result);


            var PriceCenter = await _priceCenterItemRepository.GetByPriceCenterTitle("send");
            var totalCartAmount = cart.Sum(c => (c.ProductAmount * c.Value));
            var FinalAmount = cart.Sum(c => (c.FinalAmount * c.Value));
            var extraAmount = PriceCenter.Sum(c => c.Amount);
            var User = await _userRepository.TableNoTracking.Where(c => c.Id == userId)
                .Select(x => new UserDTO() { FirstName = x.FirstName, LastName = x.LastName, PhoneNumber = x.PhoneNumber, DateInsert = DateTime.Now, Representative = x.Representative }).SingleOrDefaultAsync();
            result = true;
            //var order = await _orderRepository.CreateOrder(data, userId, totalCartAmount, FinalAmount, extraAmount, cancellationToken);
            //var details = await _orderDetailRepository.AddOrderDetails(order.Id, cart, cancellationToken);
            var DisCount = totalCartAmount - FinalAmount;
            FinalAmount = FinalAmount + extraAmount;
            return Json(new { cart, totalCartAmount, DisCount, extraAmount, FinalAmount, User, result });

        }


        [Authorize(Roles = "Client")]
        public async Task<JsonResult> CheckCopon(string Copon, CancellationToken cancellationToken)
        {
            var message = "";
            var Val = false;
            double discount = 0;
            var cart = await _cartRepository.GetCart();
            var totalCartAmount = cart.Sum(c => (c.ProductAmount * c.Value));
            var FinalAmount = cart.Sum(c => (c.FinalAmount * c.Value));
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var PriceCenter = await _priceCenterItemRepository.GetByPriceCenterTitle("send");
            var extraAmount = PriceCenter.Sum(c => c.Amount);

            if (FinalAmount != totalCartAmount)
            {
                discount = totalCartAmount - FinalAmount;
                FinalAmount = FinalAmount + extraAmount;
                return Json(new { Val, message = "محصولات تخفیف خورده", FinalAmount, discount });
            }


            var checkCopon = await _discountRepository.CheckCopon(Copon, userId);
            if (checkCopon == null)
            {
                discount = totalCartAmount - FinalAmount;
                FinalAmount = FinalAmount + extraAmount;
                return Json(new { Val, message = "کد نامعتبر می باشد", FinalAmount, discount });
            }

            if (checkCopon.TypeOffPrice == TypeOffPrice.amount)
            {
                discount = checkCopon.Value.Value;
                FinalAmount = (FinalAmount - checkCopon.Value.Value) + extraAmount;
            }
            else
            {
                discount = (FinalAmount * (checkCopon.Value.Value / 100));
                FinalAmount = FinalAmount + extraAmount - (FinalAmount * (checkCopon.Value.Value / 100));
            }
            Val = true;
            return Json(new { Val, message = "کد اعمال شد", FinalAmount, discount });
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Bill(BillDTO data, CancellationToken cancellationToken)
        {
            try
            {
                if (data.PaymentType != Commons.PaymentType.olinePay && data.PaymentType != Commons.PaymentType.wallet)
                {
                    _toastNotification.AddErrorToastMessage("نوع پرداخت راانتخاب نمایید");
                    return Redirect("Index");
                }
                var userId = HttpContext.User.Identity.GetUserId<int>();
                var cart = await _cartRepository.GetCart();
                if (cart == null)
                {
                    _toastNotification.AddErrorToastMessage("سبد خرید خالی می باشد");
                    return RedirectToAction("Index", "Cart");
                }
                if (cart.Count == 0)
                {
                    _toastNotification.AddErrorToastMessage("سبد خرید خالی می باشد");
                    return RedirectToAction("Index", "Cart");
                }

                if (data.AddressId == 0 || data.AddressId == null)
                {
                    _toastNotification.AddErrorToastMessage(" خطا در آدرس");
                    return Redirect("Index");
                }


                var PriceCenter = await _priceCenterItemRepository.GetByPriceCenterTitle("send");
                var totalCartAmount = cart.Sum(c => (c.ProductAmount * c.Value));
                var FinalAmount = cart.Sum(c => (c.FinalAmount * c.Value));
                var extraAmount = PriceCenter.Sum(c => c.Amount);
                if (data.IsNewAddress)
                {
                    var address = data.Address.ToEntity(_mapper);
                    await _addressRepository.AddAsync(address, cancellationToken);
                    data.AddressId = address.Id;
                }

                var order = await _orderRepository.CreateOrder(data, userId, totalCartAmount, FinalAmount, extraAmount, cancellationToken);
                if (order.TotalDiscount == 0 && data.DisCountCode != null)
                {
                    var checkCopon = await _discountRepository.CheckCopon(data.DisCountCode, order.UserId);
                    order = await _orderRepository.ApplyCode(order.Id, checkCopon, cancellationToken);
                }
                if (data.UserRepresentative != null)
                {
                    if (data.UserRepresentative.Trim() != "")
                    {
                        var representattiveCode = await _userRepository.UdateRepresentative(userId, data.UserRepresentative, cancellationToken);
                    }
                }

                var details = await _orderDetailRepository.AddOrderDetails(order.Id, cart, cancellationToken);


                return await Payment(order.FinalPayment, order.PaymentType.Value, order.Id, cancellationToken);
            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("خطا در صدور فاکتور");
                return Redirect("index");
            }

        }


        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Payment(double amount, PaymentType paymentType, int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var detail = await _orderDetailRepository.GetDetailbyOrderId(orderId);
                var chkwarhousebeforPayment = await _productWareHouseRepository.LastCheckBeforPayment(detail, cancellationToken);
                if (chkwarhousebeforPayment.Status == true)
                {
                    if (paymentType == PaymentType.olinePay)
                    {
                        var userId = HttpContext.User.Identity.GetUserId<int>();
                        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
                        string[] metadata = new string[2];
                        metadata[0] = user.PhoneNumber;
                        metadata[1] = user.Email;
                        var payment = await _Payment.Request(new Dto.Payment.DtoRequest()
                        {
                            Amount = (int)amount,
                            Description = $"پرداخت شماره فاکتور ",
                            CallbackUrl = "https://Moonlar.ir/Order/VerifyPayment?orderId=" + orderId,
                            MerchantId = "3a5accf4-58d7-4a2d-a32d-eb6fcb59f75d",
                            Mobile = user.PhoneNumber,
                            Email = user.Email
                        }, ZarinPal.Class.Payment.Mode.zarinpal);
                        if (payment.Status == 100)
                        {
                            return Redirect($"https://zarinpal.com/pg/StartPay/{payment.Authority}");
                        }
                        else
                        {
                            _toastNotification.AddErrorToastMessage("error " + payment.Errors);
                            return Redirect("Index");

                        }

                    }
                    else if (paymentType == PaymentType.wallet)
                    {
                        return await VerifyPayment(orderId, cancellationToken);
                    }
                    else
                    {
                        _toastNotification.AddErrorToastMessage("نوع پرداخت مشخص نشده است ");
                        return Redirect("Index");
                    }
                }
                else
                {
                    _toastNotification.AddErrorToastMessage(chkwarhousebeforPayment.Messages);
                    return Redirect("Index");
                }

            }
            catch (Exception)
            {
                _toastNotification.AddErrorToastMessage("خطا هنگام ورود به درگاه ");
                return Redirect("Index");
            }
        }



        public async Task<IActionResult> VerifyPayment(int orderId, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(cancellationToken, orderId);
                var detail = await _orderDetailRepository.GetDetailbyOrderId(order.Id);
                var userId = HttpContext.User.Identity.GetUserId<int>();
                // string authorityverify;
                if (order.PaymentType == PaymentType.olinePay)
                {
                    if (HttpContext.Request.Query["Authority"] != "")
                    {
                        authority = HttpContext.Request.Query["Authority"];
                    }


                    string url = "https://api.zarinpal.com/pg/v4/payment/verify.json?merchant_id=" +
                        "3a5accf4-58d7-4a2d-a32d-eb6fcb59f75d" + "&amount="
                        + order.FinalPayment + 0 + "&authority="
                        + authority;

                    var client = new RestClient(url);
                    Method method = Method.Post;
                    var request = new RestRequest("", method);

                    request.AddHeader("accept", "application/json");

                    request.AddHeader("content-type", "application/json");

                    var response = client.ExecuteAsync(request);


                    Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(response.Result.Content);
                    string data = jodata["data"].ToString();

                    Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(response.Result.Content);
                    string errors = jo["errors"].ToString();

                    if (data != "[]")
                    {
                        string refid = jodata["data"]["ref_id"].ToString();
                        order.RefId = "ON-" + refid;
                        order.IsFinaly = true;
                        await _orderRepository.UpdateAsync(order, cancellationToken);
                        await _productWareHouseRepository.UpdateWareHouseAfterSale(detail, cancellationToken);
                        _cartRepository.ResetCart();

                        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
                        if (user.Representative != null)
                        {
                            var representattiveRule = await _identificationCodeRuleRepository.GetRule();
                            if (representattiveRule.Status == true)
                            {
                                if (representattiveRule.Data.ForHasDisCount == false && order.TotalDiscount != 0)
                                {

                                }
                                else
                                {
                                    var checkUsedUser = await _identificationCode_LogRepository.CheckUserUsedInRule(representattiveRule.Data.Id, userId);
                                    if (checkUsedUser.Status == true)
                                    {
                                        if (representattiveRule.Data.ForOwner)
                                        {
                                            var owner = await _userRepository.GetOwnerIdByRepresentativeCode(user.Representative);
                                            await _walletRepository.ChargeWallet(owner, representattiveRule.Data.Value.Value, "شارژ هدیه کد معرف صاحب کد - " + representattiveRule.Data.Id, cancellationToken);

                                        }
                                        if (representattiveRule.Data.ForUser)
                                        {
                                            await _walletRepository.ChargeWallet(userId, representattiveRule.Data.Value.Value, "شارژ هدیه کد معرف مصرف کننده - " + representattiveRule.Data.Id, cancellationToken);
                                            await _identificationCode_LogRepository.Create(representattiveRule.Data.Id, userId, cancellationToken);
                                        }

                                    }

                                }

                            }
                        }


                        return RedirectToAction("Factor", new { OrderId = order.Id });

                    }
                    else if (errors != "[]")
                    {
                        string errorscode = jo["errors"]["code"].ToString();
                        if (errorscode == "-51")
                        {
                            _toastNotification.AddWarningToastMessage("انصراف پرداخت ");
                            return Redirect("Index");

                        }
                        else
                        {
                            _toastNotification.AddWarningToastMessage("خطا پرداخت");
                            return Redirect("Index");
                        }

                    }
                    else
                    {
                        _toastNotification.AddWarningToastMessage("خطا پرداخت");
                        return Redirect("Index");
                    }

                }
                else if (order.PaymentType == PaymentType.wallet)
                {
                    var resWallet = await _walletRepository.Pay(order.FinalPayment, userId, cancellationToken);
                    if (!resWallet)
                    {
                        _toastNotification.AddSuccessToastMessage("موجودی کافی نیست ");
                        return Redirect("Index");
                    }

                    else
                    {
                        order.RefId = "WA-" + _orderRepository.CreateRefCode();
                        order.IsFinaly = true;
                        await _orderRepository.UpdateAsync(order, cancellationToken);
                        await _productWareHouseRepository.UpdateWareHouseAfterSale(detail, cancellationToken);
                        _cartRepository.ResetCart();


                        var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
                        if (user.Representative != null)
                        {
                            var representattiveRule = await _identificationCodeRuleRepository.GetRule();
                            if (representattiveRule.Status == true)
                            {
                                if (representattiveRule.Data.ForHasDisCount == false && order.TotalDiscount != 0)
                                {

                                }
                                else
                                {
                                    var checkUsedUser = await _identificationCode_LogRepository.CheckUserUsedInRule(representattiveRule.Data.Id, userId);
                                    if (checkUsedUser.Status == true)
                                    {
                                        if (representattiveRule.Data.ForOwner)
                                        {
                                            var owner = await _userRepository.GetOwnerIdByRepresentativeCode(user.Representative);
                                            await _walletRepository.ChargeWallet(owner, representattiveRule.Data.Value.Value, "شارژ هدیه کد معرف صاحب کد - " + representattiveRule.Data.Id, cancellationToken);

                                        }
                                        if (representattiveRule.Data.ForUser)
                                        {
                                            await _walletRepository.ChargeWallet(userId, representattiveRule.Data.Value.Value, "شارژ هدیه کد معرف مصرف کننده - " + representattiveRule.Data.Id, cancellationToken);
                                            await _identificationCode_LogRepository.Create(representattiveRule.Data.Id, userId, cancellationToken);
                                        }

                                    }

                                }

                            }
                        }


                        return RedirectToAction("Factor", new { OrderId = order.Id });
                    }

                }
                else
                {
                    return Redirect("Index");
                }


            }
            catch (Exception ex)
            {
                return Redirect("Index");
                // throw new Exception(ex.Message);
            }

        }


        [Authorize(Roles = "Client")]
        public async Task<JsonResult> GetCurrentUserOrder(SearchDTO model)
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            OrderDTO orderDTO = new OrderDTO() { UserId = userId, IsFinaly = true };
            var data = await _orderRepository.GetListOrder(model, orderDTO);
            return Json(new { total = data.TotalPages, data = data.Resualt });

        }
        [Authorize(Roles = "Client")]
        public async Task<JsonResult> GetCurrentUserProductRefrence()
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var data = await _orderRepository.GetListProductReference(userId);
            return Json(data);
        }

        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Factor(int OrderId)
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var data = await _orderRepository.GetOrderById(userId, OrderId);
            if (data == null)
            {
                _toastNotification.AddErrorToastMessage("فاکتور موجود می باشد");
                return RedirectToAction("Index");
            }
            data.listOrderDetail = await _orderDetailRepository.GetDetailbyOrderId(data.Id);
            return View(data);
            //return View();
        }


    }
}
