using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts.Order;
using Data.Contracts.User;
using Data.Contracts.Wallet;
using Data.DTO;
using Data.DTO.Product;
using Data.DTO.Sales;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.SMS;
using SmsIrRestfulNetCore;
using static Common.AllEnum.Commons;

namespace DashBoard.Controllers.Order
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly ISmsService _smsService;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IUserRepository _userRepository;

        public OrderController(
            IOrderRepository orderRepository, 
            ISmsService smsService,
            IOrderDetailRepository orderDetailRepository ,
            IWalletRepository walletRepository , 
            IUserRepository userRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _walletRepository = walletRepository;
            _userRepository = userRepository;
            _smsService = smsService;
        }
        public IActionResult Index(int state , string refId)
        {
            var order = new OrderDTO() {
            State= (Common.AllEnum.Commons.SaleState)state,
            IsFinaly=true,
            RefId = refId

            };

            return View(order);
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, OrderDTO orderDTO, CancellationToken cancellationToken)
        {
            var dto = await _orderRepository.GetListOrder(model, orderDTO);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public async Task<IActionResult> Factor(int Id)
        {
            var data = await _orderRepository.GetDetail(Id);
            data.listOrderDetail = await _orderDetailRepository.GetDetailbyOrderId(data.Id);
            return View(data);
        }

        public async Task<IActionResult> ConfirmFactor(int Id , string state, string message, CancellationToken cancellationToken)
        {
            var data = await _orderRepository.GetByIdAsync(cancellationToken, Id);
            switch (data.State)
            {
                case SaleState.Pending:
                    data.State = SaleState.Accepted;
                    break;
                case SaleState.Accepted:
                    data.State = SaleState.Posted;
                    var user = await _userRepository.GetByIdAsync(cancellationToken, data.UserId);
                    var smsDATA = new SmsDTO() { templateId = 52250 };
                    var smsItem1 = new Parmeter()
                    {
                        name = "FullName",
                        value = $"{ user.FirstName} { user.LastName}"
                    };
                    smsDATA.parameters.Add(smsItem1);
                    var smsItem2 = new Parmeter()
                    {
                        name = "RefID",
                        value = data.RefId
                    };
                    smsDATA.parameters.Add(smsItem2);

                    _smsService.SendSMS(data.User.PhoneNumber, smsDATA);

                    break;
                case SaleState.ReturnedRequest:
                    if (state== "yes")
                    {
                        data.State = SaleState.ReturnedConfirmation;
                        data.Memo = data.Memo + " توضیحات ادمین : " + message;
                        var chargeWallet = await _walletRepository.ChargeWallet(data.UserId,data.FinalPayment,"بازگشت هزینه مرجوع کالا به کیف پول", cancellationToken);

                    }
                    else if (state=="no")
                    {
                        data.State = SaleState.ReturnedRejected;
                        data.Memo = data.Memo + " توضیحات ادمین : " + message;
                    }

                    break;
                default:
                    break;
            }

            await _orderRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Order", new {data.State});
        }


        public async Task<JsonResult> Delete(int Id)
        {

            return Json(true);
        }

    }
}
