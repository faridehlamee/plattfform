using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts.BaseProduct;
using Data.Contracts.Product;
using Data.DTO.BaseDTO;
using Data.DTO.BaseProduct;
using Data.DTO.Product;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.BaseProduct
{
    public class ProductDetailRepository : Repository<ProductDetail>, IProductDetailRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IDetailsRepository _detailsRepository;

        public ProductDetailRepository(IMapper Mapper, IDetailsRepository DetailsRepository, RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
            _detailsRepository = DetailsRepository;
        }

        public ProductDetailDTO GetbyDetaiIdAndProductId(int detailId , int ProductId)
        {
            try
            {
                var data = TableNoTracking.ProjectTo<ProductDetailDTO>(_mapper.ConfigurationProvider).Where(c => c.ProductId == ProductId).ToList();
                var item = data.Where(x => x.DetailsItemDetailsId == detailId).First();
                return item;
            }
            catch (Exception)
            {
                var data = new ProductDetailDTO();
                return data;
            }
        }

        public async Task<List<ProductDetailDTO>> GetByProductId(int productid)
        {
            var data = await TableNoTracking.ProjectTo<ProductDetailDTO>(_mapper.ConfigurationProvider).Where(c => c.ProductId == productid).
                Select(x => new ProductDetailDTO
                {
                    DetailsItemSubTopic = x.DetailsItemSubTopic,
                    DetailTitle = x.DetailsItemDetailsTitle
                }).ToListAsync();

            //var data2 = await TableNoTracking.ProjectTo<ProductDetailDTO>(_mapper.ConfigurationProvider).Where(c => c.ProductId == productid).
            //  ToListAsync();

            return data;
        }



        public ProductDetailDTO GetbyDetaiItemIdAndProductId(int detailItemId, int ProductId)
        {
            try
            {
                var data = TableNoTracking.ProjectTo<ProductDetailDTO>(_mapper.ConfigurationProvider).Where(c => c.DetailsItemId == detailItemId && c.ProductId == ProductId).SingleOrDefault();
                return data;
            }
            catch (Exception)
            {
                var data = new ProductDetailDTO();
                return data;
            }
        }
        public async Task<List<FilterDTO>> GetFilter(int[] Productids)
        {
            var query = TableNoTracking.Where(c => Productids.Contains(c.ProductId));
            var listDetailItem =await query.Select(c => c.DetailsItemId).Distinct().ToListAsync();
            var listDetail = await query.Where(x=> x.DetailsItem.Details.IsActive).Select(c => c.DetailsItem.Details.Id).Distinct().ToListAsync();
            var data  = await  _detailsRepository.TableNoTracking.Where(c=> listDetail.Contains(c.Id)).ProjectTo<DetailsDTO>(_mapper.ConfigurationProvider).
            Select(c => new FilterDTO
            {
                Title = c.Title,
                Item = c.ListDetailsItem.Where(c => listDetailItem.Contains(c.Id)).Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.SubTopic }).ToList()
            })

                .ToListAsync();

           
            return data;
        }

        
    }
}
