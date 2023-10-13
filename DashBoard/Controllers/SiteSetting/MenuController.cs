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
    public class MenuController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<StoreType> _storeTypeRepository;


        public MenuController(IMapper Mapper, 
            IRepository<StoreType> StoreTypeRepository,
             IImageRepository ImageRepository,
            IMenuRepository MenuRepository)
        {
            _mapper = Mapper;
            _storeTypeRepository = StoreTypeRepository;
            _imageRepository = ImageRepository;
            _menuRepository = MenuRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _menuRepository.TableNoTracking.ProjectTo<MenuDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public  IActionResult Create()
        {
            var data = new MenuDTO();
              return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MenuDTO model)
        {
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/menu/", form);
                model.Image = imagename;
            }
            var data = model.ToEntity(_mapper);
            await _menuRepository.AddAsync(data, CancellationToken.None);
            await _menuRepository.ReloadData();
            return RedirectToAction("Index", "Menu");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _menuRepository.TableNoTracking.ProjectTo<MenuDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(MenuDTO model, CancellationToken cancellationToken)
        {
            var data = await _menuRepository.GetByIdAsync(cancellationToken, model.Id);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/menu/");
                }
                var imagename = _imageRepository.SaveStaticFile("/menu/", form);
                data.Image = imagename;
            }

            data = model.ToEntity(_mapper, data);
            await _menuRepository.UpdateAsync(data, cancellationToken);
            await _menuRepository.ReloadData();
            return RedirectToAction("Index", "Menu");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _menuRepository.GetByIdAsync(cancellationToken, Id);
            await _menuRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
