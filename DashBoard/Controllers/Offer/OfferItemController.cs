using AutoMapper;
using Common.Utilities;
using Data.Contracts.OfferItem;
using Data.Contracts.Product;
using Data.DTO.Offer;
using Data.DTO.Product;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Offer
{
    [Authorize(Roles = "Admin")]
    public class OfferItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IOfferItemRepository _offerItemRepository;

        public OfferItemController(IMapper Mapper , IProductRepository ProductRepository , IOfferItemRepository OfferItemRepository)
        {
            _mapper = Mapper;
            _productRepository = ProductRepository;
            _offerItemRepository = OfferItemRepository;
        }
        public IActionResult IndexAdd(int OfferId)
        {
            var model = new OfferItemDTO();
            model.OfferId = OfferId;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddItem(OfferItemDTO data, CancellationToken cancellationToken)
        {
            //if (data.Value ==null || data.TypeOffPrice == null)
            //    return Json(false);

            //data.ExpireDate = data.PersianExpireDate.GetGregorianDate();

            var ids = data.ProductIds.Split(',');
            foreach (var item in ids)
            {
                var model = data.ToEntity(_mapper);
                model.ProductId = Convert.ToInt32(item);
                var hasExist = await _offerItemRepository.IsExist(model.OfferId.Value, model.ProductId.Value);
                if (hasExist == false)
                {
                    await _offerItemRepository.AddAsync(model, cancellationToken);
                }
            }

            return Json(true);
        }

        public IActionResult IndexRemove(int OfferId)
        {
            var model = new OfferItemDTO();
            model.OfferId = OfferId;
            return View(model);
        }
        public async Task<JsonResult> Delete(OfferItemDTO data, CancellationToken cancellationToken)
        {
            try
            {
                var AllData = new List<OfferItem>();
                var ids = data.ProductIds.Split(',');
                foreach (var item in ids)
                {
                    var id = Convert.ToInt32(item);
                    var offeritem = await _offerItemRepository.GetByProductAndOfferId(id, data.OfferId.Value);
                    offeritem.IsActive = false;
                    AllData.Add(offeritem);
                }
                await _offerItemRepository.UpdateRangeAsync(AllData, cancellationToken);
                return Json(true);
            }
            catch (Exception)
            {

                return Json(false);
            }
   
        }
        public async Task<JsonResult> List(CancellationToken cancellationToken, int Id)
        {
            var dto = await _offerItemRepository.GetByOfferId(Id);
            return Json(dto);
        }
        public async Task<JsonResult> ListForAddAsync(SearchDTO model, ProductDTO Search , CancellationToken cancellationToken)
        {
            var ExistProductId = await _offerItemRepository.GetUsedProductId(Search.OfferId);
           Search.listNotExist = ExistProductId;
            var dto = await _productRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }
       
    }
}
