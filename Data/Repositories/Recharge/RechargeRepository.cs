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
using Data.Contracts.Recharge;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Recharge;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Recharge
{
    public class RechargeRepository : Repository<Entites.Entities.Recharge>, IRechargeRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public RechargeRepository(KiatechDbContext dbContext , IMapper mapper ,IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
            _mapper = mapper;
        }


        public async Task<bool> AddToReChatge(RechargeDTO model, CancellationToken cancellationToken)
        {
            try
            {
                if (model.ProductWareHouseId == 0)
                    return false;
                if (model.PhonNumber == null || model.PhonNumber =="")
                    return false;
                var check = await CheckBeforSave(model, cancellationToken);
                if (check)
                    return false;

                var data = model.ToEntity(_mapper);
                await AddAsync(data, cancellationToken);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
           
            
        }
      

        public async Task<Pagedata<RechargeDTO>> GetListRecharge(SearchDTO model, RechargeDTO rechargeDTO)
        {
            var data = new Pagedata<RechargeDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive && c.IsCheck==false);
            query = Filter(query, rechargeDTO);
            data.Resualt = await query.ProjectTo<RechargeDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(c=> c.DateInsert)
                .Skip(model.take * (model.page - 1))
                .Take(model.take).ToListAsync();
            data.TotalPages = await query.CountAsync();

            return data;
        }
        public IQueryable<Entites.Entities.Recharge> Filter(IQueryable<Entites.Entities.Recharge> query, RechargeDTO rechargeDTO)
        {
            if (rechargeDTO.PhonNumber != null)
            {
                query = query.Where(c => c.PhonNumber.Contains(c.PhonNumber));
            }
           

            return query;

        }

        public async Task<List<Entites.Entities.Recharge>> UpdateInventoryAnnouncement( CancellationToken cancellationToken)
        {
            var data = await Table.
                Where(c => c.ProductWareHouse.value > 0 && c.IsCheck==false)
                .Include(x=> x.ProductWareHouse)
                .Include(m=> m.ProductWareHouse.TypeSizeItem).ToListAsync();
            data.ForEach(c => c.IsCheck = true);
            await UpdateRangeAsync(data, cancellationToken);
            return data;
        }

        public async Task<bool> CheckBeforSave(RechargeDTO model, CancellationToken cancellationToken)
        {
            var data = await TableNoTracking.Where(c => c.ProductWareHouseId == model.ProductWareHouseId && c.PhonNumber == model.PhonNumber).AnyAsync();
            return data;

        }
    }
}
