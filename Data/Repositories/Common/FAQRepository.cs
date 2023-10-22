using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entites.Entities;
using Data.Contracts.Common;
using AutoMapper;
using Data.DTO.Common;
using AutoMapper.QueryableExtensions;

namespace Data.Repositories.Public
{
    public class FAQRepository : Repository<FAQ>, IFAQRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public FAQRepository(RoyalCanyonDBContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<List<FAQDTO>> GetAll()
        {
            return await TableNoTracking.Where(c=> c.IsActive).ProjectTo<FAQDTO>(_mapper.ConfigurationProvider).ToListAsync();
        }




    }
}
