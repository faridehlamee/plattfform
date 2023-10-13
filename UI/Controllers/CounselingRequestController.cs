using Data.Contracts;
using Data.Contracts.Request;
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
    public class CounselingRequestController : Controller
    {
        private readonly ICounselingRequestService  _counselingRequestService;
        private readonly IRepository<Entites.Entities.Service> _serviceRepository;
        private readonly IToastNotification _toastNotification;

        public CounselingRequestController(ICounselingRequestService  counselingRequestService,
            IRepository<Entites.Entities.Service> serviceRepository ,
            IToastNotification toastNotification)
        {
            this._counselingRequestService = counselingRequestService;
            this._serviceRepository = serviceRepository;
            this._toastNotification = toastNotification;
        }
        public async Task<IActionResult> Index()
        {
            var data = new CounselingRequestDTO();
            data.ListCourse = await _serviceRepository.TableNoTracking.Where(c => c.IsActive ).Select(c => new Microsoft.AspNetCore.Mvc.Rendering.SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            }).ToListAsync();

            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CounselingRequestDTO model , CancellationToken cancellationToken)
        {
           var result =  await _counselingRequestService.CreateCounselingRequest(model, cancellationToken);
            _toastNotification.AddSuccessToastMessage(result.Messages);
            return Redirect("Index");
        }
    }
}
