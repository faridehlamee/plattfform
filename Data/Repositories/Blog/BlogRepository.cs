using Common;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
 
using Data.DTO.Product;
using Data.DTO.BaseDTO;
using System;
using Data.Contracts.Blog;
using Data.DTO.Blog;

namespace Data.Repositories.Blog
{
    public class BlogRepository : Repository<Entites.Entities.Blog>, IBlogRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        public BlogRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<Pagedata<BlogDTO>> GetPaging(SearchDTO model, BlogDTO Search)
        {
            var data = new Pagedata<BlogDTO>();
            var query = TableNoTracking.Where(c => c.IsActive);
            query = await Filter(model, query, Search);
            data.Resualt = await query.Select(c => new BlogDTO
            {
                Id = c.Id, 
                Title=c.Title,
                BlogCategoryName=c.BlogCategory.Name,
                Image=c.Image,
                Sumary=c.Sumary,
                DateInsert = c.DateInsert
            }).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;
        }
        public async Task<IQueryable<Entites.Entities.Blog>> Filter(SearchDTO model, IQueryable<Entites.Entities.Blog> query, BlogDTO Search)
        {

            if (Search.BlogCategoryIds != null)
            {
                query = query.Where(c => (Search.BlogCategoryIds.Contains(c.BlogCategoryId)));
            }
          
            return query;
        }


    }
}
