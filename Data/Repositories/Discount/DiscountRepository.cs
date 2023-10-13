using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.Discount;
using Data.Contracts.Order;
using Data.DTO.BaseDTO;
using Data.DTO.Discount;
using Data.DTO.Product;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Discount
{
    public class DiscountRepository : Repository<Entites.Entities.Discount.Discount>, IDiscountRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly IDiscountCodeUsedRepository _discountCodeUsedRepository;

        public DiscountRepository(
            IMapper Mapper ,
            KiatechDbContext dbContext, 
            IHttpContextAccessor contextAccessor,
            IOrderRepository orderRepository,
            IDiscountCodeUsedRepository discountCodeUsedRepository


            )
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
            this._orderRepository = orderRepository;
            this._discountCodeUsedRepository = discountCodeUsedRepository;
        }

        public async Task<Pagedata<DiscountDTO>> GetList(SearchDTO model, DiscountDTO filter) 
        {
            var data = new Pagedata<DiscountDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive);
            query = Filter(query,filter);

            data.Resualt = await query.ProjectTo<DiscountDTO>(_mapper.ConfigurationProvider).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
 
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;

          
        }

        public  IQueryable<Entites.Entities.Discount.Discount> Filter (IQueryable<Entites.Entities.Discount.Discount> query , DiscountDTO model)
        {
            if (model.DiscountType != 0)
            {
                query = query.Where(c => c.DiscountType== model.DiscountType);
            }
            


            return query;

        }


        public async Task<int> GetNumberAllow(int id)
        {
            return await TableNoTracking.Where(c => c.Id == id).Select(x => x.PermittedNumber.Value).SingleOrDefaultAsync();
        }

        public async ValueTask<Entites.Entities.Discount.Discount> CheckCopon(string copon, int UserId)
        {
            try
            {
                var data = await TableNoTracking.Where(c => c.IsActive && c.KeyDiscountPercent == copon).SingleOrDefaultAsync();
                if (data == null)
                    return null;
                if (data.ExpireDate < DateTime.Now)
                    return null;

                var numberUserd = await _orderRepository.CountCoponUsedById(data.Id, UserId);
                if (data.ForAll.Value == true)
                {
                    if (numberUserd >= data.PermittedNumber)
                    {
                        return null;
                    }
                    else
                    {
                        return data;
                    }
                }
                else
                {
                    var userCode = await _discountCodeUsedRepository.GetUserCode(UserId, data.Id);
                    if (userCode != null)
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task<DiscountDTO> GetByProductId(int ProductId)
        {
            var data = await TableNoTracking.ProjectTo<DiscountDTO>(_mapper.ConfigurationProvider).Where(x => x.IsActive && x.ProductId == ProductId).SingleOrDefaultAsync();
            return data;

        }

        public async Task<List<DiscountDTO>> GetDiscountCodeList()
        {
            var data = await TableNoTracking.Where(c => c.IsActive && c.KeyDiscountPercent != null)
                .Select(x => new DiscountDTO
                {
                    Id = x.Id,
                    TypeOffPrice = x.TypeOffPrice,
                    Value = x.Value,
                    KeyDiscountPercent = x.KeyDiscountPercent,
                    ExpireDate = x.ExpireDate,
                    ForAll = x.ForAll,
                    PermittedNumber = x.PermittedNumber
                }).ToListAsync();
            return data;
        }
        public async Task IsExistThenRemove(int productId , CancellationToken cancellationToken)
        {
            var data = await TableNoTracking.Where(c => c.IsActive && c.ProductId == productId).FirstOrDefaultAsync();
            if (data != null)
            {
                data.IsActive = false;
                await UpdateAsync(data, cancellationToken);
            }
        }


    }
}
