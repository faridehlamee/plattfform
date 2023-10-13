using Data.DTO.User;
using Entites.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DashBoard.Controllers
{
  
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;

        public AccountController(SignInManager<User> SignInManager)
        {
            _signInManager = SignInManager;
        }
        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public IActionResult test()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Login(LoginDTO data, string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Account");
            }
            data.url = returnUrl;
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> GetLoginAsync(LoginDTO data)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(data.PhoneNumber, data.PassWord, data.RememberMe, true);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(data.url))
                        if (Url.IsLocalUrl(data.url))
                            return Redirect(data.url);

                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index", "Home");
                    else
                        return RedirectToAction("Index", "Home");


                }
                if (result.IsLockedOut)
                {
                    ViewData["Error"] = "اکانت به دلیل پنج بار ورود غیر موفق قفل شده است";
                    return RedirectToAction("Login", "Account");
                }
                ModelState.AddModelError("", "رمز عبور یا نام کاربری اشتباه است");
            }
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

      

    }
}
