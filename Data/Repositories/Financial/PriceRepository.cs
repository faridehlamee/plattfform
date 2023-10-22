using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts.Financial;
using Data.Contracts.Product;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Financial
{
    public class PriceRepository : Repository<Price_Log>, IPriceRepository, IScopedDependency
    {
        public PriceRepository(RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
        }

        public async Task<Price_Log> GetPrice(int productId)
        {
            var data = await TableNoTracking.Where(c => c.IsActive && c.ProductId == productId).SingleOrDefaultAsync();
            return data;
        }
        
        public async Task AddNewPrice(int productId , double amount  , CancellationToken cancellationToken)
        {

            var latestPrice = await GetPrice(productId);
            if (latestPrice !=null)
            {
                latestPrice.IsActive = false;
                await UpdateAsync(latestPrice, cancellationToken);
            }
          
            var newprice = new Price_Log()
            {
                ProductId = productId,
                Amount = amount
            };
            await AddAsync(newprice, cancellationToken);

        }


    }
}
