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
using Entites.Entities.WareHouse;
using Data.Contracts.WareHouse;
using Entities.Entities.WareHouse;
using Data.DTO.WareHouse;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Data.Contracts;

namespace Data.Repositories.WareHouse
{
    public class TypeSizeItemRepository : Repository<TypeSizeItem>, ITypeSizeItemRepository, IScopedDependency
    {
        private readonly IRepository<StoreType> _storeTypeRepository;
        private readonly IRepository<Entites.Entities.Product.Product> _productRepository;
        private readonly IMapper _mapper;

        public TypeSizeItemRepository(RoyalCanyonDBContext dbContext ,IRepository<StoreType> StoreTypeRepository, 
            IRepository<Entites.Entities.Product.Product> ProductRepository, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _storeTypeRepository = StoreTypeRepository;
            _productRepository = ProductRepository;
            _mapper = Mapper;
        }
        public async Task<List<SelectListItem>> GetbyProductId(int ProductId)
        {
            var TypeSizeId = await _productRepository.TableNoTracking.Where(c=> c.Id== ProductId).Select(c=> c.TypeSizeId).FirstOrDefaultAsync();
            var data = await TableNoTracking.ProjectTo<TypeSizeItemDTO>(_mapper.ConfigurationProvider).Where(c => c.TypeSizeId == TypeSizeId && c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();
            return data;
        }

    }
}
