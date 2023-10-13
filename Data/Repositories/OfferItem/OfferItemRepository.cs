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
using Data.Contracts.Discount;
using Data.Contracts.Offer;
using Data.Contracts.OfferItem;
using Data.Contracts.Order;
using Data.DTO.Offer;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.OfferItem
{
    public class OfferItemRepository : Repository<Entites.Entities.OfferItem>, IOfferItemRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public OfferItemRepository(
            IMapper Mapper,
            KiatechDbContext dbContext, 
            IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }

        public async Task<List<int?>> GetUsedProductId(int offerId)
        {
            var data = await TableNoTracking.Where(x => x.IsActive && x.OfferId == offerId).Select(c => c.ProductId).ToListAsync();
            return data;
       
        }
        public async Task<List<OfferItemDTO>> GetByOfferId(int Id)
        {
            var data = await TableNoTracking.ProjectTo<OfferItemDTO>(_mapper.ConfigurationProvider).Where(x => x.OfferId==Id && x.IsActive).ToListAsync();
            return data;

        }
        public async Task<Entites.Entities.OfferItem> GetByProductAndOfferId(int ProductId , int offerId)
        
        {
            var data = await TableNoTracking.Where(x => x.OfferId == offerId && x.ProductId == ProductId && x.IsActive).FirstOrDefaultAsync();
            return data;
        }
        public async Task<bool> IsExist(int offerid , int productid)
        {
            return await TableNoTracking.Where(c => c.ProductId == productid && c.OfferId == offerid && c.IsActive).AnyAsync();
        }
     

      


    }
}
