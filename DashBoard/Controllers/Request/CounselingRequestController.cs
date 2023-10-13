using Data.DTO.Product;
using Data.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Request
{
    [Authorize(Roles = "Admin")]
    public class CounselingRequestController : Controller
    {
        private readonly ICounselingRequestService _requestService;

        public CounselingRequestController(ICounselingRequestService requestService)
        {
            this._requestService = requestService;
        }
        public IActionResult Index(int index)
        {
            var Request = new CounselingRequestDTO()
            {
                Index = index

            };

            return View(Request);
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, CounselingRequestDTO  counselingRequestDTO, CancellationToken cancellationToken)
        {
            counselingRequestDTO.EntityType = "CounselingRequest";
            var dto = await _requestService.GetPaging(model, counselingRequestDTO);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public async Task<IActionResult> RequestInfo(int Id)
        {
            var data = await _requestService.GetById(Id, "CounselingRequest");
            return View(data);
        }

        public async Task<IActionResult> Operation(int Id, int status, string message, CancellationToken cancellationToken)
        {
            await _requestService.UpdateStatuse(Id, status, message, "CounselingRequest");



            return RedirectToAction("Index", new { index = status });
        }
        public IActionResult Downlod(string fileName)
        {
            var path = HttpContext.Request.Host + "/images/request/" + fileName;
            var res = PhysicalFile(path, "text/plain", fileName);
            return View(res);


        }

    }
}
