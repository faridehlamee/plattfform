using Data.Contracts.User;
using Data.DTO.Product;
using Data.DTO.User;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.UserAccount
{
    public class IdentificationCodeRuleController : Controller
    {
        private readonly IIdentificationCodeRuleRepository _identificationCodeRuleRepository;

        public IdentificationCodeRuleController(IIdentificationCodeRuleRepository identificationCodeRuleRepository)
        {
            _identificationCodeRuleRepository = identificationCodeRuleRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, IdentificationCodeRuleDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _identificationCodeRuleRepository.GetList(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }


    }
}
