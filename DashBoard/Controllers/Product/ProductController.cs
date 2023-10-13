using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.BaseProduct;
using Data.Contracts.Common;
using Data.Contracts.Financial;
using Data.Contracts.Product;
using Data.DTO.BaseProduct;
using Data.DTO.Common;
using Data.DTO.Product;
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

namespace DashBoard.Controllers.Product
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IRepository<Guide> _sizeguidRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<StoreTypeDetail> _storeTypeDetailRepository;
        private readonly IDetailsItemRepository _detailsItemRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly IRepository<TypeSize> _typeSizeRepository;

        public ProductController(IMapper Mapper, IProductRepository ProductRepository, IRepository<Guide> guidRepository, 
            IProductTypeRepository ProductTypeRepository ,IPriceRepository PriceRepository,
            IImageRepository ImageRepository , IRepository<StoreTypeDetail> StoreTypeDetailRepository , IDetailsItemRepository DetailsItemRepository,
            IProductDetailRepository ProductDetailRepository, IRepository<TypeSize> TypeSizeRepository
            )
        {
            _mapper = Mapper;
            _productRepository = ProductRepository;
            _sizeguidRepository = guidRepository;
            _productTypeRepository = ProductTypeRepository;
            _priceRepository = PriceRepository;
            _imageRepository = ImageRepository;
            _storeTypeDetailRepository = StoreTypeDetailRepository;
            _detailsItemRepository = DetailsItemRepository;
            _productDetailRepository = ProductDetailRepository;
            _typeSizeRepository = TypeSizeRepository;
        }
        public IActionResult Index(int StoreTypeId) {
            var data = new ProductDTO() { StoreTypeId = StoreTypeId };
            return View(data);
        }
        public async Task<JsonResult> ListAsync( SearchDTO model , ProductDTO Search, CancellationToken cancellationToken )
        {
          
            var dto = await _productRepository.GetPaging(model , Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }

        public async Task<IActionResult> Create(ProductDTO model) 
        {

            model.StoreTypeId = model.StoreTypeId;
            model.ProductTypeList = await _productTypeRepository.GetByStoreTypeId(model.StoreTypeId);
               
          

            model.GuideList = await _sizeguidRepository.TableNoTracking.Where(c => c.IsActive && c.StoreTypeId == model.StoreTypeId).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Description
            }).ToListAsync();

            model.TypeSizeList = await _typeSizeRepository.TableNoTracking.Where(c => c.IsActive).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToListAsync();

            var image = new ImageDTO();
            model.ListImage.Add(image);

            var DetailAllowcate =await _storeTypeDetailRepository.TableNoTracking.Where(c => c.IsActive && c.StoreTypeId == model.StoreTypeId).Include(x=> x.Details).ProjectTo<StoreTypeDetailDTO>(_mapper.ConfigurationProvider).ToListAsync();
            model.ListParameter = (from rw in DetailAllowcate.AsEnumerable()
                                   select new ProductDetailDTO()
                                   {
                                       DetailId = rw.DetailsId,
                                       DetailTitle = rw.Details.Title,
                                       ListDetailItem = _detailsItemRepository.GetbyDetaiId(rw.DetailsId)
                                   }).ToList();

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProductDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _productRepository.AddAsync(data, CancellationToken.None);
            var price = new Price_Log()
            {
                ProductId = data.Id,
                Amount = data.Amount
            };
            await _priceRepository.AddAsync(price, CancellationToken.None);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                await _imageRepository.SaveImageAsync("/product/", "Product", data.Id, model.ListImage.Select(c=> c.Priority).ToArray(), form, CancellationToken.None);
            }

            var listparameter = new List<ProductDetail>();
            foreach (var item in model.ListParameter)
            {
                if (item.DetailsItemId != 0)
                {
                    var parameter = new ProductDetail();
                    parameter.ProductId = data.Id;
                    parameter.DetailsItemId = item.DetailsItemId;
                    listparameter.Add(parameter);
                }
            }
            await _productDetailRepository.AddRangeAsync(listparameter, CancellationToken.None);

            var currentImage = await _imageRepository.GetbyCurrentImage(data.Id, "Product", CancellationToken.None);
            data.CurrentImage = currentImage;
            await _productRepository.UpdateAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Product",new { StoreTypeId =data.StoreTypeId});
        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _productRepository.TableNoTracking.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            data.ProductTypeList = await _productTypeRepository.GetByStoreTypeId(data.StoreTypeId);
          
            data.GuideList = await _sizeguidRepository.TableNoTracking.Where(c => c.IsActive && c.StoreTypeId == data.StoreTypeId).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Description
            }).ToListAsync();
            data.TypeSizeList = await _typeSizeRepository.TableNoTracking.Where(c => c.IsActive).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToListAsync();

            data.Gallery = await _imageRepository.TableNoTracking.ProjectTo<ImageDTO>(_mapper.ConfigurationProvider)
          .Where(c => c.EntityId == data.Id && c.EntityType == "Product").ToListAsync();
            var image = new ImageDTO();
            data.ListImage.Add(image);


            var DetailAllowcate = await _storeTypeDetailRepository.TableNoTracking.Where(c => c.IsActive && c.StoreTypeId == data.StoreTypeId).Include(x => x.Details).ProjectTo<StoreTypeDetailDTO>(_mapper.ConfigurationProvider).ToListAsync();
            data.ListParameter = (from rw in DetailAllowcate.AsEnumerable()
                                   select new ProductDetailDTO()
                                   {
                                       Id = _productDetailRepository.GetbyDetaiIdAndProductId(rw.DetailsId, data.Id).Id,
                                       DetailsItemId = _productDetailRepository.GetbyDetaiIdAndProductId(rw.DetailsId , data.Id).DetailsItemId,
                                       DetailId = rw.DetailsId,
                                       DetailTitle = rw.Details.Title,
                                       ListDetailItem = _detailsItemRepository.GetbyDetaiId(rw.DetailsId)
                                   }).ToList();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ProductDTO model, CancellationToken cancellationToken)
        {
            var data = await _productRepository.GetByIdAsync(cancellationToken, model.Id);
          
            if (data.Amount != model.Amount)
            {
                await _priceRepository.AddNewPrice(data.Id , model.Amount, cancellationToken);
            }
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                await _imageRepository.SaveImageAsync("/product/", "Product", data.Id, model.ListImage.Select(c => c.Priority).ToArray(), form, CancellationToken.None);
            }

            var currentImage = await _imageRepository.GetbyCurrentImage(data.Id, "Product", CancellationToken.None);
            data.CurrentImage = currentImage;
            await _productRepository.UpdateAsync(data, cancellationToken);

            var listNewparameter = new List<ProductDetail>();
            var listOldparameter = new List<ProductDetail>();
            foreach (var item in model.ListParameter)
            {
                if (item.DetailsItemId != 0)
                {
                    if (item.Id != 0)
                    {
                        var oldParameter =await _productDetailRepository.GetByIdAsync(CancellationToken.None, item.Id);
                        oldParameter.DetailsItemId = item.DetailsItemId;
                        listOldparameter.Add(oldParameter);
                    }
                    else
                    {
                        var parameter = new ProductDetail();
                        parameter.ProductId = data.Id;
                        parameter.DetailsItemId = item.DetailsItemId;
                        listNewparameter.Add(parameter);
                    }
                }
            }
            if (listNewparameter.Count !=0)
            {
                await _productDetailRepository.AddRangeAsync(listNewparameter, CancellationToken.None);
            }
            if (listOldparameter.Count !=0)
            {
                await _productDetailRepository.UpdateRangeAsync(listOldparameter, CancellationToken.None);
            }
            


            return RedirectToAction("Index", "Product", new { StoreTypeId = data.StoreTypeId });
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _productRepository.GetByIdAsync(cancellationToken, Id);
            await _productRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }


        public async Task<IActionResult> AddImageForm(ProductDTO Model , string url)
        {
            Model.ProductTypeList = await _productTypeRepository.GetByStoreTypeId(Model.StoreTypeId);
            
            Model.GuideList = await _sizeguidRepository.TableNoTracking.Where(c=> c.IsActive && c.StoreTypeId== Model.StoreTypeId).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Description
            }).ToListAsync();

            var image = new ImageDTO();
            Model.ListImage.Add(image);

            var DetailAllowcate = await _storeTypeDetailRepository.TableNoTracking.Where(c => c.StoreTypeId == Model.StoreTypeId).Include(x => x.Details).ProjectTo<StoreTypeDetailDTO>(_mapper.ConfigurationProvider).ToListAsync();
            Model.ListParameter = (from rw in DetailAllowcate.AsEnumerable()
                                  select new ProductDetailDTO()
                                  {
                                      Id = _productDetailRepository.GetbyDetaiIdAndProductId(rw.DetailsId, Model.Id).Id,
                                      DetailsItemId = _productDetailRepository.GetbyDetaiIdAndProductId(rw.DetailsId, Model.Id).DetailsItemId,
                                      DetailId = rw.DetailsId,
                                      DetailTitle = rw.Details.Title,
                                      ListDetailItem = _detailsItemRepository.GetbyDetaiId(rw.DetailsId)
                                  }).ToList();

            return PartialView("_ProductForm", Model);


        }

        public async Task<IActionResult> DeleteImage(int Id , string URL)
        {
            await _imageRepository.DeleteImage(Id, "/product/");
            return Redirect(URL);
        }
    }
}
