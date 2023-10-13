using Data.Contracts;
using Data.Contracts.Common;
using Data.Contracts.Offer;
using Data.Contracts.Report;
using Data.DTO.Common;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NToastNotify;
using Services.Services.Email;
using Services.Services.UIIndex;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UI.Models;

namespace UI.Controllers
{

    public class HomeController : Controller
    {
 
        private readonly IFAQRepository _fAQRepository;
        private readonly IIndexService _indexService;
        private readonly IToastNotification _toastNotification;
        private readonly IRepository<Team> _teamRepository;
        private readonly IViewPageRepository _viewPageRepository;
        private readonly IEmailService _emailService;

        public HomeController(ILogger<HomeController> logger ,
            IFAQRepository FAQRepository,
            IIndexService  indexService,
             IToastNotification toastNotification, 
             IRepository<Team> teamRepository,
             IViewPageRepository viewPageRepository,
             IEmailService emailService)
        {
 
            _fAQRepository = FAQRepository;
            _indexService = indexService;
            this._toastNotification = toastNotification;
            this._teamRepository = teamRepository;
            this._viewPageRepository = viewPageRepository;
            this._emailService = emailService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {

            await _viewPageRepository.Add(cancellationToken);
           var data = await _indexService.GetData();
            return View(data);
        }



        public async Task<IActionResult> FAQ()
        {
            var data = await _fAQRepository.GetAll();
            return View(data);
        }
        public async Task<IActionResult> ContactUs()
        {
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailDTO email , CancellationToken cancellationToken)
        {
            await _emailService.Create(email, cancellationToken);
            var notifToInfo = new EmailDTO();
            notifToInfo = email;
            notifToInfo.Replay = $"Email = {email.EmailAddress}  message={email.message}";
            notifToInfo.EmailAddress = "info@kiatechsoftware.com";
            await _emailService.SendEmail(notifToInfo,cancellationToken);
            _toastNotification.AddSuccessToastMessage("Submitted successfully!");
            return RedirectToAction("ContactUs", "Home");

        }

        public async Task<IActionResult> OrderRequest()
        {

            return View();
        }
        public async Task<IActionResult> PaymentMethod()
        {

            return View();
        }
        public async Task<IActionResult> ReturnedProduct()
        {

            return View();
        }
        
        public async Task<IActionResult> AssistCoach()
        {

            return View();
        }
        public async Task<IActionResult> Requitment()
        {

            return View();
        }
        public async Task<IActionResult> AboutUs()
        {
            var Teams = await _teamRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new TeamDTO
            {

                Id = c.Id,
                FullName = c.FullName,
                Image = c.Image,
                JobDescription = c.JobDescription,
                Bio = c.Bio,
                LinkedIdUrl = c.LinkedIdUrl
            }).ToListAsync();
            return View(Teams);
        }
 



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
