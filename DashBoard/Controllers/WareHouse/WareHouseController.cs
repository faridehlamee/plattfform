using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.Product;
using Data.Contracts.WareHouse;
using Data.DTO.WareHouse;
using Data.Repositories;
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
    public class WareHouseController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductWareHouseRepository _productWareHouseRepository;
        private readonly ITypeSizeItemRepository _typeSizeItemRepository;
        private readonly IProductRepository _productRepository;

        public WareHouseController(IMapper Mapper, IProductWareHouseRepository productWareHouseRepository ,
            ITypeSizeItemRepository TypeSizeItemRepository , IProductRepository ProductRepository 
            )
        {
            _mapper = Mapper;
            _productWareHouseRepository = productWareHouseRepository;
            _typeSizeItemRepository = TypeSizeItemRepository;
            _productRepository = ProductRepository;
        }
        public IActionResult Index(int ProductId) 
        { 
            //var data = 
            return View(ProductId);
        }

        public async Task<JsonResult> ListAsync(int Id ,CancellationToken cancellationToken)
        {
            var dto = await _productWareHouseRepository.TableNoTracking.ProjectTo<ProductWareHouseDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.ProductId == Id)
                .OrderBy(x=> x.Id)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create(int ProductId)
        {
            var model = new ProductWareHouseDTO();
            model.ProductId = ProductId;
            model.ListTypeItem = await _typeSizeItemRepository.GetbyProductId(ProductId);



            return View(model); 
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductWareHouseDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _productWareHouseRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "WareHouse" , new { ProductId =data.ProductId});

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var model = await _productWareHouseRepository.TableNoTracking.ProjectTo<ProductWareHouseDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            model.ListTypeItem = await _typeSizeItemRepository.GetbyProductId(model.ProductId);

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductWareHouseDTO model, CancellationToken cancellationToken)
        {
            var data = await _productWareHouseRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _productWareHouseRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "WareHouse", new { ProductId = data.ProductId });
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _productWareHouseRepository.GetByIdAsync(cancellationToken, Id);
            await _productWareHouseRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
