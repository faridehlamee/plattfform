using AutoMapper;
using Common;
using Data.Contracts.User;
using Data.Contracts.Wallet;
using Entites.Entities.Wallet;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ZarinPal.Class;

namespace UI.Controllers
{
    public class WalletController : Controller
    {
        private Payment _Payment;
        string authority;
        private readonly IWalletRepository _walletRepository;
        private readonly IWalletHistoryRepository _walletHistoryRepository;
        private readonly IUserRepository _userRepository;
        private readonly IToastNotification _toastNotification;

        public WalletController(IMapper Mapper, IWalletRepository WalletRepository, 
            IWalletHistoryRepository walletHistoryRepository,
            IUserRepository userRepository,
             IToastNotification toastNotification)
        {
            _walletRepository = WalletRepository;
            _walletHistoryRepository = walletHistoryRepository;
            _userRepository = userRepository;
            _toastNotification = toastNotification;
            var expose = new Expose();
            _Payment = expose.CreatePayment();
        }
        [HttpPost("Payment")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Payment(double amount, CancellationToken cancellationToken)
        {
            try
            {
                var userId = HttpContext.User.Identity.GetUserId<int>();
                var user = await _userRepository.GetByIdAsync(cancellationToken, userId);
                var wallet = await _walletRepository.GetbyUserId(userId);
                string[] metadata = new string[2];
                metadata[0] = user.PhoneNumber;
                metadata[1] = user.Email;
                var walletLog = new WalletHistory()
                {
                    Balance = wallet.Balance,
                    Amount = amount,
                    Operation = "+",
                    WalletId = wallet.Id,
                    Status = 0,
                    StatusDesc = "هنگام پرداخت"
                };
                await _walletHistoryRepository.AddAsync(walletLog, cancellationToken);
                //string requesturl;
                //requesturl = "https://api.zarinpal.com/pg/v4/payment/request.json?merchant_id=" +
                //    "d0b65091-487c-4687-94c7-b7491d8f32b2" + "&amount=" + amount + 0 +
                //    "&callback_url=" + "http://beniix.com/Wallet/VerifyPayment?LogwalletId=" + walletLog.Id +
                //    "&description=" + " درگاه پرداخت کیف پول"
                //   + "&metadata[0]=" + metadata[0] + "& metadata[1]=" + metadata[1];
                //;
                //var client = new RestClient(requesturl);
                //Method method = Method.Post;
                //var request = new RestRequest("", method);

                //request.AddHeader("accept", "application/json");

                //request.AddHeader("content-type", "application/json");

                //var requestresponse = client.ExecuteAsync(request);

                //Newtonsoft.Json.Linq.JObject jo = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Result.Content);
                //string errorscode = jo["errors"].ToString();

                //Newtonsoft.Json.Linq.JObject jodata = Newtonsoft.Json.Linq.JObject.Parse(requestresponse.Result.Content);
                //string dataauth = jodata["data"].ToString();

                //if (dataauth != "[]")
                //{
                //    authority = jodata["data"]["authority"].ToString();
                //    string gatewayUrl = "https://www.zarinpal.com/pg/StartPay/" + authority;
                //    return Redirect(gatewayUrl);
                //}
                //else
                //{
                //    //return BadRequest();
                //    _toastNotification.AddErrorToastMessage("خطا شارژ"+errorscode);
                //    return RedirectToAction("Index", "User");
                //    //return BadRequest("error " + errorscode);

                //}


                var payment = await _Payment.Request(new Dto.Payment.DtoRequest()
                {
                    Amount = (int)amount,
                    Description = $"شارژ کیف پول",
                    CallbackUrl = "https://beniix.com/Wallet/VerifyPayment?LogwalletId="+ walletLog.Id,
                    MerchantId = "d0b65091-487c-4687-94c7-b7491d8f32b2",
                    Mobile = user.PhoneNumber,
                    Email = user.Email
                }, ZarinPal.Class.Payment.Mode.zarinpal);
                if (payment.Status == 100)
                {
                    return Redirect($"https://zarinpal.com/pg/StartPay/{payment.Authority}");
                }
                else
                {
                       _toastNotification.AddErrorToastMessage("خطا شارژ"+ payment.Status);
                       return RedirectToAction("Index", "User");

                }
            }
            catch (Exception ex)
            {
                _toastNotification.AddErrorToastMessage("خطا شارژ");
                return RedirectToAction("Index", "User");
            }
        }


        public async Task<IActionResult> VerifyPayment(int LogwalletId, CancellationToken cancellationToken)
        {

            var walletlog = await _walletHistoryRepository.GetByIdAsync(cancellationToken, LogwalletId);
            var wallet = await _walletRepository.GetByIdAsync(cancellationToken, walletlog.WalletId);
            if (wallet == null)
            {

                walletlog.Status = 500;
                walletlog.StatusDesc = "خطا کیف پول";

                await _walletHistoryRepository.UpdateAsync(walletlog, cancellationToken);
                _toastNotification.AddErrorToastMessage("خطا کیف پول" + 500);
                return RedirectToAction("Index", "User");
            }
            try
            {
                if (HttpContext.Request.Query["Authority"] != "")
                {
                    authority = HttpContext.Request.Query["Authority"];
                }

                string url = "https://api.zarinpal.com/pg/v4/payment/verify.json?merchant_id=" +
                    "d0b65091-487c-4687-94c7-b7491d8f32b2" + "&amount="
                    + walletlog.Amount + 0 + "&authority="
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
                //var wallet = new Entites.Entities.Wallet.Wallet();
                //wallet = await _walletRepository.GetbyUserId(userId);
                if (data != "[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();


                    wallet.Balance = wallet.Balance + walletlog.Amount;
                    await _walletRepository.UpdateAsync(wallet, cancellationToken);

                    walletlog.Status = 200;
                    walletlog.StatusDesc = "شارژ از طریق درگاه انجام شد";
                    await _walletHistoryRepository.UpdateAsync(walletlog, cancellationToken);

                    _toastNotification.AddSuccessToastMessage("شارژ انجام شد" );
                    return RedirectToAction("Index", "User");
                }
                else if (errors != "[]")
                {
                    string errorscode = jo["errors"]["code"].ToString();
                    walletlog.Status = 0;
                    walletlog.StatusDesc = "خطا در شارژ درگاه" + errorscode;
                    _toastNotification.AddErrorToastMessage("خطا در شارژ");
                    return RedirectToAction("Index", "User");

                }
                walletlog.Status = 0;
                walletlog.StatusDesc = "خطا در شارژ درگاه";
                await _walletHistoryRepository.UpdateAsync(walletlog, cancellationToken);
                _toastNotification.AddErrorToastMessage("خطا در شارژ");
                return RedirectToAction("Index", "User");

            }
            catch (Exception ex)
            {

                // throw new Exception(ex.Message);
                _toastNotification.AddErrorToastMessage("خطا در شارژ");
                return RedirectToAction("Index", "User");
            }

        }

       
        [Authorize(Roles = "Client")]
        public async Task<JsonResult> GetCurrentUserBalance()
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var data = await _walletRepository.GetbyUserId(userId);
            return Json(data.Balance);
        }

    }
}
