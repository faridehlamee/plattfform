using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts;
using Data.Contracts.Order;
using Data.Contracts.User;
using Data.Contracts.Wallet;
using Data.DTO.Address;
using Data.DTO.Sales;
using Data.DTO.User;
using Entites.Entities;
using Entites.Entities.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace UI.Controllers
{
    public class UserController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;
        private readonly IOrderRepository _orderRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IWalletRepository _walletRepository;

        public UserController(IMapper Mapper, IUserRepository UserRepository, SignInManager<User> SignInManager, IOrderRepository OrderRepository, IAddressRepository AddressRepository,
            IRepository<Province> ProvinceRepository, IRepository<City> CityRepository , IWalletRepository walletRepository)
        {
            _mapper = Mapper;
            _userRepository = UserRepository;
            _signInManager = SignInManager;
            _orderRepository = OrderRepository;
            _addressRepository = AddressRepository;
            _provinceRepository = ProvinceRepository;
            _cityRepository = CityRepository;
            _walletRepository = walletRepository;
        }
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> Index()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userIdInt = HttpContext.User.Identity.GetUserId<int>();
                var user = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == userIdInt).FirstOrDefaultAsync();
                var balnce = await _walletRepository.GetbyUserId(userIdInt);
                user.WalletBalnce = balnce.Balance;
                return View(user);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Orders()
        {
            if (_signInManager.IsSignedIn(User))
            {
               
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> Address()
        {
            if (_signInManager.IsSignedIn(User))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> ChangePassword()
        {
            if (_signInManager.IsSignedIn(User))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Programs()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userIdInt = HttpContext.User.Identity.GetUserId<int>();
                ///var user = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == userIdInt).FirstOrDefaultAsync();
                var balnce = await _walletRepository.GetbyUserId(userIdInt);
                // user.WalletBalnce = balnce.Balance;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Favorite()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userIdInt = HttpContext.User.Identity.GetUserId<int>();
                ///var user = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == userIdInt).FirstOrDefaultAsync();
                var balnce = await _walletRepository.GetbyUserId(userIdInt);
                // user.WalletBalnce = balnce.Balance;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> Information()
        {
            if (_signInManager.IsSignedIn(User))
            {
                var userIdInt = HttpContext.User.Identity.GetUserId<int>();
                ///var user = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == userIdInt).FirstOrDefaultAsync();
                var balnce = await _walletRepository.GetbyUserId(userIdInt);
                // user.WalletBalnce = balnce.Balance;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public async Task<IActionResult> AdditionalInfo()
        {
            var userIdInt = HttpContext.User.Identity.GetUserId<int>();
            var user = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.Id == userIdInt).FirstOrDefaultAsync();
            user.address = _addressRepository.TableNoTracking.Where(c => c.IsActive && c.UserId == userIdInt).ProjectTo<AddressDTO>(_mapper.ConfigurationProvider)
                .FirstOrDefault();
            if (user.address == null)
            {
                user.address = new AddressDTO();

            }
            return View(user);
        }


        [Authorize(Roles = "Client")]
        public async Task<IActionResult> UpdateAccount(UserDTO model , CancellationToken cancellationToken)
        {
            model.Id = HttpContext.User.Identity.GetUserId<int>();

            var res = await _userRepository.UpdateAccountForPayment(model, cancellationToken);

            await _addressRepository.CreateAndUpdateAddress(model.address, cancellationToken);
         
            return Redirect("Index");
        }
        public async Task<IActionResult> EditAdress(int Id)
        {
            var data = await _addressRepository.TableNoTracking.ProjectTo<AddressDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == Id).FirstOrDefaultAsync();


            //data.ProvinceList = await _provinceRepository.TableNoTracking
            //   .Where(c => c.IsActive)
            //   .Select(s => new SelectListItem
            //   {
            //       Value = s.Id.ToString(),
            //       Text = s.Name
            //   })
            //   .ToListAsync();
            //data.CityList = await _cityRepository.TableNoTracking
            //   .Where(c => c.IsActive)
            //   .Select(s => new SelectListItem
            //   {
            //       Value = s.Id.ToString(),
            //       Text = s.Name
            //   })
            //   .ToListAsync();
            return View(data);
        }


        public async Task<ActionResult> OrderDetail(int Id)
        {
            var order = await _orderRepository.TableNoTracking.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).Where(c => c.Id == Id).FirstOrDefaultAsync();
            return PartialView("_orderItem", order);
        }

        public async Task<IActionResult> GetAddress(CancellationToken cancellationToken)
        {
            var userId = HttpContext.User.Identity.GetUserId<int>();
            var data = await _addressRepository.GetUserAddressList(userId, cancellationToken);
            return Json(data);

        }


        public async Task<IActionResult> Course()
        {
            if (_signInManager.IsSignedIn(User))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public async Task<IActionResult> Exam()
        {
            if (_signInManager.IsSignedIn(User))
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

    }

}
