using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using System;
using Data.Contracts.Blog;
using Data.DTO.Blog;

namespace Data.Repositories.Blog
{
    public class BlogCategoryRepository : Repository<Entites.Entities.BlogCategory>, IBlogCategoryRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public BlogCategoryRepository(RoyalCanyonDBContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<Pagedata<BlogCategoryDTO>> GetPaging(SearchDTO model, BlogCategoryDTO Search)
        {
            var data = new Pagedata<BlogCategoryDTO>();
            var query = TableNoTracking.Where(c => c.IsActive);
            data.Resualt = await query.Select(c => new BlogCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                DateInsert = c.DateInsert
            }).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;
        }



    }
}
