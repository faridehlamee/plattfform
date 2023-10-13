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
using Data.Contracts.Financial;
using Data.Contracts.Product;
using Data.DTO.Financial;
using Entites.Entities;
using Entites.Entities.Financial;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Financial
{
    public class PriceCenterItemRepository : Repository<PriceCenterItem>, IPriceCenterItemRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public PriceCenterItemRepository(KiatechDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = mapper;
        }

        public async Task<List<PriceCenterItemDTO>> GetByPriceCenterTitle(string Code)
        {
            var data =await TableNoTracking.Where(c => c.PriceCenter.Code == Code).ProjectTo<PriceCenterItemDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return data;
        }


    }
}
