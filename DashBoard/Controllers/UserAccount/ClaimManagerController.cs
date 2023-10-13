﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoard.Controllers.UserAccount
{
    [Authorize(Roles = "Admin")]
    public class ClaimManagerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
