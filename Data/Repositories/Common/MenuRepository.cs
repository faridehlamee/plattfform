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
    public class MenuRepository : Repository<Menu>, IMenuRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;

        public MenuRepository(IMapper Mapper, RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor ,IMemoryCache memoryCache)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
            this._memoryCache = memoryCache;
        }
     
        public async Task<List<MenuDTO>> GetAllActive()
        {
            await ReloadData();
           // var cashData = new List<MenuDTO>();
            if (_memoryCache.TryGetValue(CacheKeys.Menu, out List<MenuDTO> cashData))
            {
                return cashData;
            }
            else
            {
                var data = await base.TableNoTracking.Where(c => c.IsActive).ProjectTo<MenuDTO>(_mapper.ConfigurationProvider).OrderBy(v => v.Level).ToListAsync();
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

           
                var data = await base.TableNoTracking.Where(c => c.IsActive).ProjectTo<MenuDTO>(_mapper.ConfigurationProvider).OrderBy(v => v.Level).ToListAsync();
                var cacheEntryOptions = new MemoryCacheEntryOptions()
                   // Set cache entry size by extension method.
                   .SetSize(1)
                   // Keep in cache for this time, reset time if accessed.
                   .SetSlidingExpiration(TimeSpan.FromMinutes(15));

                // Set cache entry size via property.
                // cacheEntryOptions.Size = 1;

                // Save data in cache.
                _memoryCache.Set(CacheKeys.Menu, data, cacheEntryOptions);
              
           


        }
        //public async Task<List<ServiceDTO>> GetServices()
        //{
        //    await ReloadData();
        //    // var cashData = new List<MenuDTO>();
        //    if (_memoryCache.TryGetValue(CacheKeys.Menu, out List<ServiceDTO> cashData))
        //    {
        //        return cashData;
        //    }
        //    else
        //    {
        //        var data = await TableNoTracking.Where(c => c.IsActive).ProjectTo<ServiceDTO>(_mapper.ConfigurationProvider).OrderBy(v => v.Name).ToListAsync();
        //        var cacheEntryOptions = new MemoryCacheEntryOptions()
        //           // Set cache entry size by extension method.
        //           .SetSize(1)
        //           // Keep in cache for this time, reset time if accessed.
        //           .SetSlidingExpiration(TimeSpan.FromMinutes(15));

        //        // Set cache entry size via property.
        //        // cacheEntryOptions.Size = 1;

        //        // Save data in cache.
        //        _memoryCache.Set(CacheKeys.Menu, data, cacheEntryOptions);
        //        return data;
        //    }


        //}



    }
}
