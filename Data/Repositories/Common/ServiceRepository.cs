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
using Entites.Entities.Menu;
using Data.DTO.Menu;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Data.Cashe;
using Data.DTO.Common;

namespace Data.Repositories.Public
{
    public class ServiceRepository : Repository<Service>, IServiceRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public ServiceRepository(IMapper Mapper, RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor ,IMemoryCache memoryCache)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
            this._memoryCache = memoryCache;
        }
     
        public async Task<List<DTO.Common.ServiceDTO>> GetServices()
        {
            await ReloadData();
           // var cashData = new List<MenuDTO>();
            if (_memoryCache.TryGetValue(CacheKeys.Menu, out List<DTO.Common.ServiceDTO> cashData))
            {
                return cashData;
            }
            else
            {
                var data = await base.TableNoTracking.Where(c => c.IsActive).ProjectTo<DTO.Common.ServiceDTO>(_mapper.ConfigurationProvider).OrderBy(v => v.Name).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   // Set cache entry size by extension method.
                   .SetSize(1)
                   // Keep in cache for this time, reset time if accessed.
                   .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                // Set cache entry size via property.
                // cacheEntryOptions.Size = 1;

                // Save data in cache.
                _memoryCache.Set(CacheKeys.Menu, data, cacheEntryOptions);
                return data;
            }
           
            
        }

        public async Task ReloadData()
        {

           
                var data = await base.TableNoTracking.Where(c => c.IsActive).ProjectTo<DTO.Common.ServiceDTO>(_mapper.ConfigurationProvider).OrderBy(v => v.Name).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   // Set cache entry size by extension method.
                   .SetSize(1)
                   // Keep in cache for this time, reset time if accessed.
                   .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                // Set cache entry size via property.
                // cacheEntryOptions.Size = 1;

                // Save data in cache.
                _memoryCache.Set(CacheKeys.Cart, data, cacheEntryOptions);
              
           


        }



    }
}
