using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.Product;
using Data.DTO.BaseProduct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Repositories.Product
{
    public class ProductTypeRepository : Repository<Entites.Entities.ProductType>, IProductTypeRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public ProductTypeRepository(IMapper Mapper, KiatechDbContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<List<SelectListItem>> GetByStoreTypeId(int id)
        {
            return await TableNoTracking.ProjectTo<ProductTypeDTO>(_mapper.ConfigurationProvider).Where(c => c.StoreTypeId == id).Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            }).ToListAsync();
        }


    }
}
