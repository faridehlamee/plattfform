using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts.Recharge;
using Data.DTO;
using Data.DTO.Product;
using Data.DTO.Recharge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.SMS;
using SmsIrRestfulNetCore;

namespace DashBoard.Controllers.Recharge
{
    [Authorize(Roles = "Admin")]
    public class RechargeController : Controller
    {
        private readonly IRechargeRepository _rechargeRepository;
        private readonly ISmsService _smsService;

        public RechargeController(IRechargeRepository rechargeRepository , ISmsService smsService)
        {
            _rechargeRepository = rechargeRepository;
            _smsService = smsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> ListAsync(SearchDTO model, RechargeDTO rechargeDTO, CancellationToken cancellationToken)
        {
            var dto = await _rechargeRepository.GetListRecharge(model, rechargeDTO);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }


        public async Task<IActionResult> InventoryAnnouncement(CancellationToken cancellationToken)
        {

            var data = await _rechargeRepository.UpdateInventoryAnnouncement(cancellationToken);
            foreach (var item in data)
            {
                var smsDATA = new SmsDTO() { templateId = 52939 };
                var smsItem = new Parmeter()
                {
                     name= "id",
                     value = item.ProductWareHouse.ProductId.ToString()
                };
                smsDATA.parameters.Add(smsItem);

                _smsService.SendSMS(item.PhonNumber, smsDATA);
            }

            return Redirect("Index");
        }

    }
}
