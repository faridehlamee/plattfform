using Data.Contracts;
using Data.DTO.Request;
using Entites.Entities.Request;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Services.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class CooperationRequestController : Controller
    {
        private readonly ICooperationRequestService  _cooperationRequestService;
        private readonly IToastNotification _toastNotification;

        public CooperationRequestController(ICooperationRequestService  cooperationRequestService, IToastNotification toastNotification)
        {
            this._cooperationRequestService = cooperationRequestService;
            this._toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var data = new CooperationRequestDTO();
            //data.ListCourse = await _courseRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            //{
            //    Text = c.Name,
            //    Value = c.Id.ToString()
            //}).ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> CreateAsync(CooperationRequestDTO model , CancellationToken cancellationToken)
        {
            await _cooperationRequestService.CreateCooperationRequest(model, cancellationToken);
            _toastNotification.AddSuccessToastMessage("درخواست باموفقیت ثبت شد");
            return Redirect("Index");
        }
    }
}
