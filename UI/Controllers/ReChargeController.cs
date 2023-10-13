using Common.Utilities;
using Data.Contracts.Recharge;
using Data.DTO.Recharge;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ReChargeController : Controller
    {
        private readonly IRechargeRepository _rechargeRepository;
        public ReChargeController(IRechargeRepository rechargeRepository)
        {
            _rechargeRepository = rechargeRepository;
        }
        [HttpPost]
        public async Task<JsonResult> recharge(int ltmSize, string Phone , CancellationToken cancellationToken)
        {
            if (ltmSize == 0 || Phone == "" || Phone == null)
                return Json(new { success = false, Message = "مقادیر خالی است" });
            if (Phone.Length != 11)
                return Json(new { success = false, Message = "شماره نا معتبر میباشد" });
            var model = new RechargeDTO() {
            ProductWareHouseId = ltmSize,
            PhonNumber = Phone.Fa2En()
            };

            var res = await _rechargeRepository.AddToReChatge(model, cancellationToken);
            if (res)
            {
                return Json(new { success = false, Message = "قبلا ثبت گردیده" });
            }
            else
            {
              
               return Json(new { success = true, Message = "باموفقیت ثبت شد" });
            }
        }
    }
}
