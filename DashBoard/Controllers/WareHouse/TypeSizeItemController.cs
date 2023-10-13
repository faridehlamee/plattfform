using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Data.DTO.WareHouse;
using Data.Repositories;
using Entites.Entities;
using Entities.Entities.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.WareHouse
{
    [Authorize(Roles = "Admin")]
    public class TypeSizeItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TypeSize> _typeSizeRepository;
        private readonly IRepository<TypeSizeItem> _typeSizeItemRepository;

        public TypeSizeItemController(IMapper Mapper, IRepository<TypeSize> TypeSizeRepository, IRepository<TypeSizeItem> TypeSizeItemRepository)
        {
            _mapper = Mapper;
            _typeSizeRepository = TypeSizeRepository;
            _typeSizeItemRepository = TypeSizeItemRepository;
        }
        public IActionResult Index(int Id) 
        {
            var data = _typeSizeRepository.GetByIdAsync(CancellationToken.None, Id).Result;
            return View(data); 
        }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken , int Id)
        {
            var dto = await _typeSizeItemRepository.TableNoTracking.ProjectTo<TypeSizeItemDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.TypeSizeId== Id)
                .ToListAsync();

            return Json(dto);
        }

        public  IActionResult Create(int TypeSizeId) 
        {
            var data = new TypeSizeItemDTO();
            data.TypeSizeId = TypeSizeId;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TypeSizeItemDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _typeSizeItemRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "TypeSizeItem", new { Id=data.TypeSizeId});

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _typeSizeItemRepository.TableNoTracking.ProjectTo<TypeSizeItemDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

          
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TypeSizeItemDTO model, CancellationToken cancellationToken)
        {
            var data = await _typeSizeItemRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _typeSizeItemRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "TypeSizeItem", new { Id = data.TypeSizeId });
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _typeSizeItemRepository.GetByIdAsync(cancellationToken, Id);
            await _typeSizeItemRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
