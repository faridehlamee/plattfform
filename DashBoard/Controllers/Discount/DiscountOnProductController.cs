using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Utilities;
using Data.Contracts.Discount;
using Data.Contracts.Offer;
using Data.Contracts.OfferItem;
using Data.Contracts.Product;
using Data.Contracts.User;
using Data.DTO.Discount;
using Data.DTO.Offer;
using Data.DTO.Product;
using Data.DTO.User;
using Entites.Entities.Discount;
using Entites.Entities.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers.Discount
{
    [Authorize(Roles = "Admin")]
    public class DiscountOnProductController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRepository;
        private readonly IDiscountCodeUsedRepository _discountCodeUsedRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public DiscountOnProductController(IDiscountRepository discountRepository, IProductRepository ProductRepository, IDiscountCodeUsedRepository discountCodeUsedRepository, IMapper Mapper, IUserRepository UserRepository)
        {
            _mapper = Mapper;
            _userRepository = UserRepository;
            _discountRepository = discountRepository;
            _productRepository = ProductRepository;
            _discountCodeUsedRepository = discountCodeUsedRepository;
        }
        public IActionResult Index(int discountType)
        {
            var data = new DiscountDTO() { DiscountType = (Common.AllEnum.Commons.DiscountType)discountType };

            return View(data);
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, DiscountDTO Search, CancellationToken cancellationToken)
        {
            Search.DiscountType = Common.AllEnum.Commons.DiscountType.onProduct;
            var dto = await _discountRepository.GetList(model, Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public IActionResult Create(int discountType)
        {
            var data = new DiscountDTO() { DiscountType = (Common.AllEnum.Commons.DiscountType)discountType };

            return View(data);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DiscountDTO data, CancellationToken cancellationToken)
        {
            if (data.Value == null || data.TypeOffPrice == null)
                return Json(false);

            data.StartDate = data.PersianStartDate.GetGregorianDate();
            data.ExpireDate = data.PersianExpireDate.GetGregorianDate();

            
            foreach (var item in data.ProductIds)
            {
                var model = data.ToEntity(_mapper);
                model.ProductId =item;
                await _discountRepository.IsExistThenRemove( model.ProductId.Value, cancellationToken);
          
                await _discountRepository.AddAsync(model, cancellationToken);
            }

            return Json(true);

        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _discountRepository.GetByIdAsync(cancellationToken, Id);
            await _discountRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }


        public async Task<JsonResult> ListForAddAsync(SearchDTO model, ProductDTO Search, CancellationToken cancellationToken)
        {
      
            var dto = await _productRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }


    }
}
