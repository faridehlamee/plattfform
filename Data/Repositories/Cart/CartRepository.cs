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
using Data.Cashe;
using Data.Contracts.BaseProduct;
using Data.Contracts.Cart;
using Data.Contracts.WareHouse;
using Data.DTO.Cart;
using Data.DTO.Offer;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace Data.Repositories.Cart
{
    public class CartRepository : Repository<Entites.Entities.Cart.Cart>, ICartRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IProductWareHouseRepository _productWareHouseRepository;
        private readonly IMemoryCache _memoryCash;
        private readonly IHttpContextAccessor _contextAccessor;

        public CartRepository(IMapper Mapper, IProductWareHouseRepository productWareHouseRepository, IMemoryCache memoryCash , RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
            _productWareHouseRepository = productWareHouseRepository;
            _memoryCash = memoryCash;
            _contextAccessor = contextAccessor;
        }

        public async Task<string> AddToCart(InsertCartDTO model)
        {
            if (model.Value == 0)
                return "مقدار انتخاب نشده است";
            if (model.ProductWareHouseId == 0)
                return "مقدار انتخاب نشده است";

            var data = model.ToEntity(_mapper);
            var selected = await TableNoTracking.Where(c => c.key == model.key && c.IsActive && c.ProductId == data.ProductId && c.ProductWareHouseId == data.ProductWareHouseId)
                .SingleOrDefaultAsync();
            if (selected !=null)
            {
                selected.Value = data.Value + selected.Value;
                var check = await _productWareHouseRepository.CheckInventory(selected.ProductWareHouseId, selected.Value);
                if (check)
                {
                    await UpdateAsync(selected, CancellationToken.None);
                    return "باموفقیت ثبت شد";
                }
                else
                {
                    return "مقدار انتخابی موجود نیست";
                }
            }
            else
            {
                var check = await _productWareHouseRepository.CheckInventory(model.ProductWareHouseId, model.Value);
                if (check)
                {
                    await AddAsync(data, CancellationToken.None);
                    return "باموفقیت ثبت شد";
                }
                else
                {
                    return "مقدار انتخابی موجود نیست";
                }
            }
          
           
            
        }


        //[ResponseCache(Location = ResponseCacheLocation.Client, NoStore = false)]
        public string getKey()
        {
            var cashData = "";
            //if (_memoryCash.TryGetValue(CacheKeys.Cart, out cashData))
            //{
            //    return cashData;
            //}
            //else
            // {
            //    var cash = Guid.NewGuid().ToString();
            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //       // Set cache entry size by extension method.
            //       .SetSize(1)
            //       // Keep in cache for this time, reset time if accessed.
            //       .SetSlidingExpiration(TimeSpan.FromMinutes(6000));

            //    // Set cache entry size via property.
            //    // cacheEntryOptions.Size = 1;

            //    // Save data in cache.
            //    _memoryCash.Set(CacheKeys.Cart, cash, cacheEntryOptions);
            //    return cash;
            //}
            cashData = _contextAccessor.HttpContext.Session.GetString(CacheKeys.Cart);
            if (cashData=="" || cashData==null)
            {
                cashData = Guid.NewGuid().ToString();
                _contextAccessor.HttpContext.Session.SetString(CacheKeys.Cart, cashData);
            }
            return cashData;





        }


        public async Task<List<CartDTO>> GetCart()
        {
            //var cashData = "df8f7d7e-1703-4caa-bd87-c0e1bc88714b";
            var cashData = getKey();

            var data = await TableNoTracking.Where(c => c.IsActive && c.key == cashData).ProjectTo<CartDTO>(_mapper.ConfigurationProvider).ToListAsync();
          
           
            return data;
        }


        public void ResetCart()
        {
            _contextAccessor.HttpContext.Session.Remove(CacheKeys.Cart);
        }



    }
}
