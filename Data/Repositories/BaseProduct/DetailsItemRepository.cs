 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts.BaseProduct;
using Data.Contracts.Product;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.BaseProduct
{
    public class DetailsItemRepository : Repository<DetailsItem>, IDetailsItemRepository, IScopedDependency
    {
        public DetailsItemRepository(KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext , contextAccessor)
        {
        }


        public List<SelectListItem> GetbyDetaiId(int detailid)
        {
            var data =  TableNoTracking.Where(c => c.DetailsId == detailid).Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.SubTopic
            }).ToList();

            return data;
        }
    }
}
