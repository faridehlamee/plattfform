using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Utilities;
using Data.Contracts.BaseProduct;
using Data.Contracts.Common;
using Data.Contracts.OfferItem;
using Data.Contracts.Product;
using Data.Contracts.WareHouse;
using Data.DTO.BaseDTO;
using Data.DTO.Discount;
using Data.DTO.Offer;
using Data.DTO.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.Repositories.Product
{
    public class ProductRepository : Repository<Entites.Entities.Product.Product>, IProductRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IProductWareHouseRepository _productWareHouseRepository;
        private readonly IOfferItemRepository _offerItemRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IProductDetailRepository _productDetailRepository;
        private readonly ICommentRepository _commentRepository;

        public ProductRepository(IMapper Mapper, IProductWareHouseRepository productWareHouseRepository, IProductDetailRepository ProductDetailRepository, ICommentRepository commentRepository,
            IOfferItemRepository OfferItemRepository  , IImageRepository imageRepository,RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
            _productWareHouseRepository = productWareHouseRepository;
            _offerItemRepository = OfferItemRepository;
            _imageRepository = imageRepository;
            _productDetailRepository = ProductDetailRepository;
            _commentRepository = commentRepository;
        }


        public async Task<List<ProductDTO>> GetAll()
        {
            var dto = await TableNoTracking.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
              .Where(c => c.IsActive ).OrderByDescending(x => x.DateInsert)
              .ToListAsync();
            return dto;
        }


        public async Task<ProductDTO> GetDetail(int id)
        {
            //var dto = await TableNoTracking.Where(c => c.IsActive && c.Id == id).Include(w => w.OfferItem.Where(a => a.IsActive)).ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
            //    .SingleOrDefaultAsync();
            var dto = await TableNoTracking.Where(c => c.IsActive && c.Id == id)
                   .Include(y => y.Discount)
                .Include(y => y.Guide)
                .ProjectTo<ProductDTO>(_mapper.ConfigurationProvider).Select(x => new ProductDTO
            {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    GuideId = x.GuideId,
                    GuideImageFile = x.Guide.ImageFile,
                    ProductTypeName = x.ProductTypeName,
                    ProductTypeId = x.ProductTypeId,
                    StoreTypeId = x.StoreTypeId,
                    StoreTypeStoreName = x.StoreTypeStoreName,
                    Amount = x.Amount,
                    Discount = (ICollection<DiscountDTO>)x.Discount.Where(c => c.IsActive).Select(x => new DiscountDTO { Value = x.Value, TypeOffPrice = x.TypeOffPrice, ExpireDate = x.ExpireDate, IsActive = x.IsActive }).ToList(),
                    SumWareHouses = (int)x.ProductWareHouses.Where(c => c.value > 0).Sum(m => m.value),
                    CurrentImage = x.CurrentImage,
                    IsShow = x.IsShow,
                    HasEmptySize = x.ProductWareHouses.Where(c => c.IsActive && c.value == 0).Any()


                }).SingleOrDefaultAsync();
            dto.ProductGallery = await _imageRepository.GetListImageByEntityId(dto.Id, "Product");
            dto.ListWareHouse = await _productWareHouseRepository.GetbyProductId(dto.Id);
            dto.ListParameter = await _productDetailRepository.GetByProductId(dto.Id);
            dto.listComment = await _commentRepository.GetListByProductId(dto.Id, CancellationToken.None);
            return dto;
        }

        public async Task<List<ProductDTO>> GetbyStoreTypeId(int storeTypeId)
        {
            var dto = await TableNoTracking.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
              .Where(c => c.IsActive && c.StoreTypeId == storeTypeId).OrderByDescending(x => x.DateInsert)
              .ToListAsync();
            return dto;
        }

        public async Task<Pagedata<BaseProductDTO>> GetPaging(SearchDTO model, ProductDTO Search)
        {
            var data = new Pagedata<BaseProductDTO>();

            var query = TableNoTracking.Where(c => c.IsActive);

            query = await Filter(model, query , Search);

            query = query.Include(y => y.Discount).Include(w=> w.details);
            data.Resualt =  await query.ProjectTo<ProductDTO>(_mapper.ConfigurationProvider)
                  .Select(c => new BaseProductDTO
                  {
                      Id = c.Id,
                      CurrentImage = c.CurrentImage,
                      Amount = c.Amount,
                      Name = c.Name,
                      Group = c.Group,
                      Discount = c.Discount.Where(c => c.IsActive).Select(x => new DiscountDTO { Value = x.Value, TypeOffPrice = x.TypeOffPrice, ExpireDate = x.ExpireDate, IsActive = x.IsActive }).ToList(),
                      SumWareHouses = (int)c.ProductWareHouses.Sum(m => m.value),
                      IsShow = c.IsShow,
                      ProductCode = c.ProductCode,
                      DateInsert = c.DateInsert
                  }).OrderBy(e => e.Group).ThenByDescending(e => e.DateInsert).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();


            //این دو خط پایین برای react
            //  var ProductIds = await query.Select(c => c.Id).ToArrayAsync();
            //data.Filter = await MakeFilter(ProductIds);
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;
        }

        public async Task<IQueryable<Entites.Entities.Product.Product>> Filter (SearchDTO model, IQueryable<Entites.Entities.Product.Product> query, ProductDTO Search)
        {
            if (model.IsShow != null)
            {
                query = query.Where(c => c.IsShow == model.IsShow);
            }
            if (model.OfferId.HasValue)
            {
                var ProductIds = await _offerItemRepository.TableNoTracking.Where(c => c.IsActive && c.OfferId == model.OfferId).Select(x => x.ProductId).ToListAsync();
                query = query.Where(c => ProductIds.Contains(c.Id));
            }
            if (model.StoreTypeId !=null)
            {
                query = query.Where(c => c.StoreTypeId == model.StoreTypeId);
            }
            if (model.ProductTypeId != null)
            {
                query = query.Where(c => c.ProductTypeId == model.ProductTypeId);
            }
            if (model.CreatorId != null)
            {
                query = query.Where(c => c.CreatorId == model.CreatorId);
            }
            if (model.Search != null)
            {
                var arry = model.Search.Split(' ');
                foreach (var item in arry)
                {
                    query = query.Where(c=> c.Name.Contains(item.CleanString()) || c.Tags.Contains(item.CleanString()));
                }
            }
            if (Search.Name != null)
            {
                query = query.Where(c => c.Name.Contains(Search.Name.CleanString()));
            }
            if (Search.ProductCode != null)
            {
                query = query.Where(c => c.ProductCode.Contains(Search.ProductCode));
            }
            if (model.D !=null)
            {

                //for (int i = 0; i < model.D.Count; i++)
                //{
                //    if (model.D[i] !=  0)
                //    {
                //        int x = model.D[i];
                //        query = query.Where(c => c.details.Select(x => x.DetailsItemId).Contains(x));
                //    }
                //}
                var productIds = await _productDetailRepository.TableNoTracking.Where(c => model.D.Contains(c.DetailsItemId)).Select(x => x.ProductId).Distinct().ToListAsync();
                query = query.Where(c => productIds.Contains(c.Id));

            }
            if (Search.listNotExist != null)
            {
                query = query.Where(c => !Search.listNotExist.Contains(c.Id));
            }


      
            return query;
        }



        public async Task<List<FilterDTO>> MakeFilter(int[] ProductIds)
        {
            var ListFilter = await _productDetailRepository.GetFilter(ProductIds);
            return ListFilter;

        }




    }
}
