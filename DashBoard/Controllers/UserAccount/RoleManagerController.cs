using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.User;
using Data.Contracts;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.UserAccount
{
    [Authorize(Roles = "Admin")]
    public class RoleManagerController : Controller
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly IRepository<Role> _roleRepository;

        public RoleManagerController(IMapper Mapper, RoleManager<Role> RoleManager , IRepository<Role> RoleRepository)
        {
            _mapper = Mapper;
            _roleManager = RoleManager;
            _roleRepository = RoleRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _roleRepository.TableNoTracking.ProjectTo<RoleDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RoleDTO model)
        {
            var data = model.ToEntity(_mapper);
            var result2 = await _roleManager.CreateAsync(data);
            return RedirectToAction("Index", "RoleManager");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _roleRepository.TableNoTracking.ProjectTo<RoleDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(RoleDTO model, CancellationToken cancellationToken)
        {
            var data = await _roleRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _roleManager.UpdateAsync(data);
            return RedirectToAction("Index", "RoleManager");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _roleRepository.GetByIdAsync(cancellationToken, Id);
            await _roleRepository.DeleteAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
