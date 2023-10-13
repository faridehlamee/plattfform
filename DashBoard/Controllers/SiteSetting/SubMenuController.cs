using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.Menu;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.SiteSetting
{
    [Authorize(Roles = "Admin")]
    public class SubMenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly ISubMenuRepository _subMenuRepository;
        private readonly IRepository<StoreType> _storeTypeRepository;
        private readonly IRepository<ProductType> _productTypeRepository;

        public SubMenuController(IMapper Mapper,
           IRepository<StoreType> StoreTypeRepository,
            IRepository<ProductType> ProductTypeRepository,
            IMenuRepository MenuRepository,
           ISubMenuRepository subMenuRepository)
        {
            _mapper = Mapper;
            _storeTypeRepository = StoreTypeRepository;
            _productTypeRepository = ProductTypeRepository;
            _menuRepository = MenuRepository;
            _subMenuRepository = subMenuRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _subMenuRepository.TableNoTracking.ProjectTo<SubMenuDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create()
        {
            var data = new SubMenuDTO();
            data.MenuList = await _menuRepository.TableNoTracking.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Title
            }).ToListAsync();
            data.ListSubMenu = await _subMenuRepository.TableNoTracking.Where(c => c.IsActive && c.ParentId == null)
          .Select(s => new SelectListItem
          {
              Value = s.Id.ToString(),
              Text = s.SubTitle
          })
          .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SubMenuDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _subMenuRepository.AddAsync(data, CancellationToken.None);
            await _menuRepository.ReloadData();
            return RedirectToAction("Index", "SubMenu");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _subMenuRepository.TableNoTracking.ProjectTo<SubMenuDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            data.MenuList = await _menuRepository.TableNoTracking.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Title
            }).ToListAsync();

            data.ListSubMenu = await _subMenuRepository.TableNoTracking.Where(c => c.IsActive && c.ParentId == null)
        .Select(s => new SelectListItem
        {
            Value = s.Id.ToString(),
            Text = s.SubTitle
        })
        .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(SubMenuDTO model, CancellationToken cancellationToken)
        {
            var data = await _subMenuRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _subMenuRepository.UpdateAsync(data, cancellationToken);
            await _menuRepository.ReloadData();
            return RedirectToAction("Index", "SubMenu");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _subMenuRepository.GetByIdAsync(cancellationToken, Id);
            await _subMenuRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
