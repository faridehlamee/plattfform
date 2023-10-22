using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.Discount;
using Data.DTO.BaseDTO;
using Data.DTO.Discount;
using Data.DTO.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Discount
{
    public class DiscountCodeUsedRepository : Repository<Entites.Entities.Discount.DiscountCodeUsed>, IDiscountCodeUsedRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public DiscountCodeUsedRepository(IMapper Mapper , RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }

        public async Task<Pagedata<DiscountCodeUsedDTO>> GetList(SearchDTO model, DiscountCodeUsedDTO filter) 
        {
            var data = new Pagedata<DiscountCodeUsedDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive);
            query = Filter(query,filter);

            data.Resualt = await query.ProjectTo<DiscountCodeUsedDTO>(_mapper.ConfigurationProvider).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
            data.TotalPages = await query.CountAsync();

            return data;
        }

        public  IQueryable<Entites.Entities.Discount.DiscountCodeUsed> Filter (IQueryable<Entites.Entities.Discount.DiscountCodeUsed> query , DiscountCodeUsedDTO model)
        {
            if (model.User.FirstName != null)
            {
                query = query.Where(c => c.User.FirstName.Contains(model.User.FirstName));
            }
            if (model.User.LastName != null)
            {
                query = query.Where(c => c.User.FirstName.Contains(model.User.LastName));

            }
            if (model.User.PhoneNumber != null)
            {
                query = query.Where(c => c.User.FirstName.Contains(model.User.PhoneNumber));

            }
            if (model.DiscountId != 0)
            {
                query = query.Where(c => c.DiscountId == model.DiscountId);
            }
            return query;

        }

        public async Task<Entites.Entities.Discount.DiscountCodeUsed> GetUserCode(int userId , int DiscountId)
        {
            var data = await TableNoTracking.Where(c => c.UserId == userId && c.DiscountId == DiscountId &&  c.IsCompleted==false).SingleOrDefaultAsync();
            return data;
        }

    }
}
