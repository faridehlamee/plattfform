using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using AutoMapper;
using Data.Cashe;
using Data.Contracts.User;
using Data.Contracts.Wallet;
using Data.DTO;
using Data.DTO.User;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Services.Services.Security.Provider;
using Services.SMS;
using SmsIrRestfulNetCore;
using static Common.AllEnum.Commons;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Common.AllEnum;
using Common.Utilities;
using Common;
using NToastNotify;
using System.Text.RegularExpressions;
using System.Security.Claims;

namespace UI.Controllers
{

    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IphonrTotpProviders _phonrTotpProviders;
        private readonly ISmsService _smsService;
        private readonly IMemoryCache _memoryCash;
        private readonly IAddressRepository _addressRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IToastNotification _toastNotification;

        public AccountController(IUserRepository UserRepository,
              ILogger<AccountController> Logger,
              UserManager<User> UserManager,
              RoleManager<Role> RoleManager,
              SignInManager<User> SignInManager,
              IMapper Mapper,
              IphonrTotpProviders phonrTotpProviders,
              ISmsService smsService,
              IMemoryCache memoryCash,
              IAddressRepository AddressRepository,
              IWalletRepository walletRepository,
               IToastNotification toastNotification)
        {
            _userRepository = UserRepository;
            _logger = Logger;
            _userManager = UserManager;
            _roleManager = RoleManager;
            _signInManager = SignInManager;
            _mapper = Mapper;
            _phonrTotpProviders = phonrTotpProviders;
            _smsService = smsService;
            _memoryCash = memoryCash;
            _addressRepository = AddressRepository;
            _walletRepository = walletRepository;
            _toastNotification = toastNotification;
        }

        public IActionResult CheckUser(TOTPDTO data)
        {
            if (_signInManager.IsSignedIn(User) && User.IsInRole("Client"))
            {
                return RedirectToAction("Index", "User");
            }
            return View(data);
        }


        [ResponseCache(Duration = 1, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> CheckToSendTOTP(TOTPDTO model)
        {
            model.PhoneNumber = model.PhoneNumber.Fa2En();
            if (model.PhoneNumber.Length != 11)
            {
                _toastNotification.AddErrorToastMessage("شماره نا معتبر");
                return Redirect("CheckUser");

            }
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "User");

            //var checkResend = _userRepository.CheckTOTPResend();
            //if (!checkResend.Status)
            //{
            //    _toastNotification.AddErrorToastMessage(checkResend.Messages);
            //    return RedirectToAction("CheckUser");
            //}

            var data = await _userRepository.CheckUniqePhoneNumber(model.PhoneNumber);
            if (data == false)
            {
                //var check = await _userRepository.CheckUserBeforLogin(model.PhoneNumber);
                //byte[] secretKey;
                //using (var rng = new RNGCryptoServiceProvider())
                //{
                //    secretKey = new byte[32];
                //    rng.GetBytes(secretKey);
                //}


                //var totpCode = _phonrTotpProviders.GenerateTotp(secretKey);

                //var smsDATA = new SmsDTO() { TemplateId = 52247 };
                //var smsItem = new UltraFastParameters()
                //{
                //    Parameter = "Code",
                //    ParameterValue = totpCode
                //};
                //smsDATA.UltraFastParameters.Add(smsItem);

                //_smsService.SendSMS(check.PhoneNumber, smsDATA);

                //model.ExpirtionTime = DateTime.Now.AddMinutes(1);
                //model.SecretKey = secretKey;
                //var cash = JsonSerializer.Serialize(model);

                //_userRepository.SaveTOTPCache(cash);

                return RedirectToAction("RegisterUser", "Account", new FirstRegisterDTO() { PhoneNumber = model.PhoneNumber });

            }
            else
            {
                return RedirectToAction("Login", "Account", new LoginDTO() { PhoneNumber = model.PhoneNumber });
                //return await SendForgetCode(new LoginDTO() { PhoneNumber = model.PhoneNumber, url = model.Url }, CancellationToken.None);
            }
        }

        public IActionResult VerifyUser()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "User");

            return View();
        }

        [HttpPost("Verify")]
        public IActionResult Verify(string Code)
        {

            if (_signInManager.IsSignedIn(User))
            {
                _toastNotification.AddWarningToastMessage("کاربر لاگین می باشد");
                return RedirectToAction("Index", "User");
            }

            var checkResend = _userRepository.CheckTOTPVerify();

            if (!checkResend.Status)
            {
                _toastNotification.AddErrorToastMessage(checkResend.Messages);
                return RedirectToAction("CheckUser", "Account");
            }
            else
            {
                var resCode = _phonrTotpProviders.VerifyTotp(checkResend.Data.SecretKey, Code);
                if (resCode.Succeeded)
                {
                    var phonenumber = checkResend.Data.PhoneNumber;
                    return RedirectToAction("RegisterUser", new FirstRegisterDTO() { PhoneNumber = phonenumber });
                }
                else
                {
                    _toastNotification.AddErrorToastMessage("کد اشتباه وارد شده");
                    return RedirectToAction("VerifyUser", "Account");
                }
            }

        }

        [HttpGet]
        public IActionResult RegisterUser(FirstRegisterDTO data)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Index", "User");

            return View(data);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(FirstRegisterDTO user, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var data = new User();
                if (user.PassWord != user.RePassWord)
                {
                    _toastNotification.AddErrorToastMessage("پسورد و تکرار پسورد برابر نیستن");
                    return RedirectToAction("RegisterUser", user);
                }
                if (user.PassWord.Length < 5)
                {
                    _toastNotification.AddErrorToastMessage("پسورد باید حداقل 6 کارکتر باشد");
                    return RedirectToAction("RegisterUser", user);
                }
                if (user.FirstName == null || user.FirstName.Trim() == "" || user.LastName == null || user.LastName.Trim() == "")
                {
                    _toastNotification.AddErrorToastMessage("نام یا نام خانوادگی را وارد نمایید");
                    return RedirectToAction("RegisterUser", user);
                }
                //var data = user.ToEntity(_mapper);
                data.FirstName = user.FirstName;
                data.LastName = user.LastName;
                data.UserName = user.PhoneNumber;
                data.PhoneNumber = user.PhoneNumber;
                data.IdentificationCode = await _userRepository.GenerateIdentifactionCode();
                data.SecurityStamp = "FWIKPZFIZ7DSVK2E5OGDHIHL4OAJZKEW";
                data.State = Commons.UserState.StepTwo;

                var result = await _userManager.CreateAsync(data);
                if (result.Succeeded)
                {
                    await _userManager.AddPasswordAsync(data, user.PassWord);
                    var createWallet = new Entites.Entities.Wallet.Wallet()
                    {
                        Balance = 0,
                        UserId = data.Id
                    };
                    await _walletRepository.AddAsync(createWallet, cancellationToken);
                    var result3 = await _userManager.AddToRoleAsync(data, "Client");
                    var logindto = new LoginDTO { PhoneNumber = user.PhoneNumber.Fa2En(), PassWord = user.PassWord };

                    return await GetLoginAsync(logindto);
                }
                _toastNotification.AddErrorToastMessage("خطا در ثبت نام");
                return RedirectToAction("RegisterUser", user);


            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                                           .SelectMany(v => v.Errors)
                                           .Select(e => e.ErrorMessage));
                _toastNotification.AddWarningToastMessage(message);
                return RedirectToAction("RegisterUser", user);
            }

        }






        public IActionResult Login(LoginDTO data, string returnUrl)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "User");
            }
            if (data.PhoneNumber == null)
            {
                return RedirectToAction("CheckUser", "Account", new { url = returnUrl });
            }

            //return RedirectToAction("CheckUser", "Account");
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> GetLoginAsync(LoginDTO data)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(data.PhoneNumber.Fa2En(), data.PassWord, data.RememberMe, true);
                if (result.Succeeded)
                {

                    if (!string.IsNullOrEmpty(data.url))
                        if (Url.IsLocalUrl(data.url))
                            return Redirect(data.url);

                    if (User.IsInRole("Admin"))
                        return RedirectToAction("Index", "User");
                    else
                        return RedirectToAction("Index", "User");


                }
                if (result.IsLockedOut)
                {
                    _toastNotification.AddErrorToastMessage("اکانت به دلیل پنج بار ورود غیر موفق قفل شده است");
                    return RedirectToAction("CheckUser", "Account");
                }
                _toastNotification.AddErrorToastMessage("رمز عبور یا نام کاربری اشتباه است");
            }
            _toastNotification.AddErrorToastMessage("رمز عبور یا نام کاربری اشتباه است");
            return RedirectToAction("CheckUser", "Account");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> ChangePassWord(string CurrentPassword, string NewPassword, string ReNewPassword)
        {
            if (NewPassword == ReNewPassword)
            {
                var userId = HttpContext.User.Identity.GetUserId<int>();
                var user = await _userManager.FindByIdAsync(userId.ToString());
                var data = await _userManager.ChangePasswordAsync(user, CurrentPassword, NewPassword);
                if (data.Errors.Count() > 0)
                {

                    _toastNotification.AddErrorToastMessage("مقادیر اشتباه می باشد");
                    return RedirectToAction("Index", "User");
                }

                _toastNotification.AddSuccessToastMessage("رمز عبور باموفقیت تغییر کرد");
                return RedirectToAction("Index", "User");
            }
            else
            {
                _toastNotification.AddErrorToastMessage("تکرار پسورد اشتباه می باشد");
                return RedirectToAction("Index", "User");
            }

        }


        //public IActionResult Forgetcode()
        //{
        //    if (_signInManager.IsSignedIn(User))
        //    {
        //        return RedirectToAction("Index", "User");
        //    }
        //    return View();
        //}

        public async Task<IActionResult> SendForgetCode(LoginDTO data, CancellationToken cancellationToken)
        {
            var code = await _userRepository.SendForgetCode(data.PhoneNumber, cancellationToken);
            if (code == "")
            {

                _toastNotification.AddErrorToastMessage("خطا در ارسال کد");
                return Redirect("Forgetcode");
            }

            //var smsDATA = new SmsDTO() { TemplateId = 52247 };
            //var smsItem = new UltraFastParameters()
            //{
            //    Parameter = "Code",
            //    ParameterValue = code
            //};
            //smsDATA.UltraFastParameters.Add(smsItem);
            //_smsService.SendSMS(data.PhoneNumber, smsDATA);
            return RedirectToAction("VerifyForgetcode", "Account", data);

        }
        public IActionResult VerifyForgetcode(LoginDTO data)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "User");
            }
            //LoginDTO data = new LoginDTO() { PhoneNumber = PhoneNumber };
            return View(data);
        }


        [HttpPost]
        public async Task<IActionResult> LoginWithForgetCode(LoginDTO data, CancellationToken cancellationToken)
        {
            // var user = await _userRepository.GetByUserAndPass(username, password, cancellationToken);
            if (User.Identity.IsAuthenticated) { return RedirectToAction("Index", "User"); }

            var user = await _userManager.FindByNameAsync(data.PhoneNumber);
            if (user == null)
            {
                _toastNotification.AddErrorToastMessage("نام کاربری یا رمز عبور اشتباه است");
                //return Redirect("Forgetcode");
                return RedirectToAction("VerifyForgetcode", data);
            }
            var IsCodeValid = await _userRepository.CheckForgetCode(data.PhoneNumber, data.PassWord);
            if (!IsCodeValid.Status)
            {
                _toastNotification.AddErrorToastMessage(IsCodeValid.Messages);
                //return Redirect("Forgetcode");
                return RedirectToAction("VerifyForgetcode", data);
            }

            await _userManager.RemovePasswordAsync(user);
            var newews = await _userManager.AddPasswordAsync(user, data.PassWord);

            if (newews.Succeeded)
            {

                return await GetLoginAsync(data);
            }
            else
            {
                _toastNotification.AddErrorToastMessage("نام کاربری یا رمز عبور اشتباه است");
                //return Redirect("Forgetcode");
                return RedirectToAction("CheckUser");
            }


        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return Redirect("CheckUser");
        }


    }


}
