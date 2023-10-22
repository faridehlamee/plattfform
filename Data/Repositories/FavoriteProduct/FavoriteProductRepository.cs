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
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.FavoriteProductDTO;
using Data.Contracts.FavoriteProduct;

namespace Data.Repositories.FavoriteProduct
{
    public class FavoriteProductRepository : Repository<Entites.Entities.FavoriteProduct.FavoriteProduct>, IFavoriteProduct, IScopedDependency
    {
        private readonly IMapper _mapper;

        public FavoriteProductRepository(RoyalCanyonDBContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }

     
    }
}
