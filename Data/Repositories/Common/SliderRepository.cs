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
using Data.DTO.Common;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Data.Repositories.Public
{
    public class SliderRepository : Repository<Slider>, ISliderRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public SliderRepository(IMapper Mapper, RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }
   
        public async Task<List<SliderDTO>> GetAllAsync()
        {
            var data = await TableNoTracking.ProjectTo<SliderDTO>(_mapper.ConfigurationProvider).Where(c => c.IsActive).OrderByDescending(x=> x.DateInsert)
                .Take(1)
                .ToListAsync();
            return data;
        }


    }
}
