using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.User;
using Data.DTO.Product;
using Data.DTO.User;
using Data.Repositories;
using Data.Repositories.User;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.UserAccount
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IMapper Mapper , UserManager<User> userManager, RoleManager<Role> roleManager , IUserRepository UserRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userRepository = UserRepository;
            _mapper = Mapper;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model,UserDTO Search ,CancellationToken cancellationToken)
        {
            var dto = await _userRepository.GetListUser(model , Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDTO model)
        {
            var data = model.ToEntity(_mapper);
            data.UserName = data.PhoneNumber;
            data.State = Common.AllEnum.Commons.UserState.StepTwo;
            var result = await _userManager.CreateAsync(data, model.PassWord);
            return RedirectToAction("Index", "User");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _userRepository.TableNoTracking.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(UserDTO model, CancellationToken cancellationToken)
        {
            var data = await _userRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _userManager.UpdateAsync(data);
            return RedirectToAction("Index", "User");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _userRepository.GetByIdAsync(cancellationToken, Id);
            await _userRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }


        [HttpGet]
        public async Task<IActionResult> AddUserToRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var roles = _roleManager.Roles.AsTracking()
                .Select(r => r.Name).ToList();
            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = roles.Where(r => !userRoles.Contains(r))
                .Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToRole(AddUserToRoleViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.AddToRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("Editpage" , new { Id=model.UserId});

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromRole(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);
            var validRoles = userRoles.Select(r => new UserRolesViewModel(r)).ToList();
            var model = new AddUserToRoleViewModel(id, validRoles);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> RemoveUserFromRole(AddUserToRoleViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestRoles = model.UserRoles.Where(r => r.IsSelected)
                .Select(u => u.RoleName)
                .ToList();
            var result = await _userManager.RemoveFromRolesAsync(user, requestRoles);

            if (result.Succeeded) return RedirectToAction("Editpage", new { Id = model.UserId });

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddUserToClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            var allClaim = ClaimStore.AllClaims;
            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims =
                allClaim.Where(c => userClaims.All(x => x.Type != c.Type))
                    .Select(c => new ClaimsViewModel(c.Type)).ToList();

            var model = new AddOrRemoveClaimViewModel(id, validClaims);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUserToClaim(AddOrRemoveClaimViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestClaims =
                model.UserClaims.Where(r => r.IsSelected)
                .Select(u => new Claim(u.ClaimType, true.ToString())).ToList();

            var result = await _userManager.AddClaimsAsync(user, requestClaims);

            if (result.Succeeded) return RedirectToAction("Editpage", new { Id = model.UserId });

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> RemoveUserFromClaim(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var userClaims = await _userManager.GetClaimsAsync(user);
            var validClaims =
                userClaims.Select(c => new ClaimsViewModel(c.Type)).ToList();

            var model = new AddOrRemoveClaimViewModel(id, validClaims);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromClaim(AddOrRemoveClaimViewModel model)
        {
            if (model == null) return NotFound();
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null) return NotFound();
            var requestClaims =
                model.UserClaims.Where(r => r.IsSelected)
                    .Select(u => new Claim(u.ClaimType, true.ToString())).ToList();

            var result = await _userManager.RemoveClaimsAsync(user, requestClaims);

            if (result.Succeeded) return RedirectToAction("Editpage", new { Id = model.UserId });

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }


        public async Task<IActionResult> ChangePassword(int Id)
        {
            var user = await _userRepository.TableNoTracking.Select(c=> new UserDTO
            { 
                Id=c.Id
            }).SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAsync(UserDTO data)
        {
            var user = await _userRepository.Table.Where(c=> c.Id==data.Id).SingleOrDefaultAsync(p => p.Id == data.Id, CancellationToken.None);
            await _userManager.RemovePasswordAsync(user);
            var newews  = await _userManager.AddPasswordAsync(user, data.PassWord);
            return Redirect("Index");
        }


    }
}
