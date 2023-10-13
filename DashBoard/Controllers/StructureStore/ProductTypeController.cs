using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Data.Repositories;
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

namespace DashBoard.Controllers.StructureStore
{
    [Authorize(Roles = "Admin")]
    public class ProductTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StoreType> _storeTypeRepository;
        private readonly IRepository<ProductType> _productTypeRepository;

        public ProductTypeController(IMapper Mapper, IRepository<StoreType> StoreTypeRepository, IRepository<ProductType> ProductTypeRepository)
        {
            _mapper = Mapper;
            _storeTypeRepository = StoreTypeRepository;
            _productTypeRepository = ProductTypeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _productTypeRepository.TableNoTracking.ProjectTo<ProductTypeDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create()
        {
            var data = new ProductTypeDTO();
            data.StoreTypeList = await _storeTypeRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StoreName
                })
                .ToListAsync();


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductTypeDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _productTypeRepository.AddAsync(data, CancellationToken.None);

            return RedirectToAction("Index", "ProductType");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _productTypeRepository.TableNoTracking.ProjectTo<ProductTypeDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            data.StoreTypeList = await _storeTypeRepository.TableNoTracking.Where(c => c.IsActive)
             .Select(s => new SelectListItem
             {
                 Value = s.Id.ToString(),
                 Text = s.StoreName
             })
             .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductTypeDTO model, CancellationToken cancellationToken)
        {
            var data = await _productTypeRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _productTypeRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "ProductType");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _productTypeRepository.GetByIdAsync(cancellationToken, Id);
            await _productTypeRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
