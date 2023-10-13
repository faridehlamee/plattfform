using Data.Contracts.BaseProduct;
using Data.Contracts.Common;
using Data.Contracts.Product;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly ICommentRepository _commentRepository;

        public ProductController(IProductRepository ProductRepository, IProductDetailRepository ProductDetailRepository )
        {
            _productRepository = ProductRepository;
            _productDetailRepository = ProductDetailRepository;
        }
        public async Task<IActionResult> Index(SearchDTO model)
        {

            var productDTO = new ProductDTO();
            var query = await _productRepository.Filter(model, _productRepository.TableNoTracking, productDTO);
            var ProductIds = await query.Select(c => c.Id).ToArrayAsync();
            model.Filter = await _productDetailRepository.GetFilter(ProductIds);


            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetProduct(SearchDTO model)
        {
            model.IsShow = true;
            var productDTO = new ProductDTO();
            var data = await _productRepository.GetPaging(model, productDTO);
            return Json(data);
        }
        public async Task<IActionResult> ProductDetail(int id)
        {
            var data = await _productRepository.GetDetail(id);
            return View(data);
        }

        public async Task<JsonResult> GetSameProduct(int storeTypeId, int productTypeId)
        {
            var productDTO = new ProductDTO();
            var model = new SearchDTO()
            {
                StoreTypeId = storeTypeId,
                ProductTypeId = productTypeId,
                take = 12

            };
            var data = await _productRepository.GetPaging(model, productDTO);
            return Json(data.Resualt);
        }
    }
}
