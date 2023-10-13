 using DashBoard.Models;
using Data.Contracts;
using Data.Contracts.Order;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Report;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoard.Controllers
{
    [Authorize(Roles ="Admin,Coach")]
    public class HomeController : Controller
    {
        private readonly IReportService _reportService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IReportService reportService , ILogger<HomeController> logger)
        {
            _reportService = reportService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var data = new Dashboard();
            data.SaleReport = await _reportService.SaleOrderReport();
            data.AllPriceReport = await _reportService.AllPriceReport();
            data.AllOrderReport = await _reportService.AllOrderReport();
            data.NewOrderReport = await _reportService.NewOrderReport();
            data.RegisterUserReport = await _reportService.RegisterReport();


            return View(data);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
