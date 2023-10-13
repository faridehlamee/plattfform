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
using Data.Contracts.Movie;
using Data.DTO.Movies;
using Data.DTO.BaseDTO;
using Data.DTO.Product;

namespace Data.Repositories.Movie
{
    public class MovieCategoryRepository : Repository<Entites.Entities.Movies.MovieCategory>, IMovieCategoryRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public MovieCategoryRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<Pagedata<MovieCategoryDTO>> GetPaging(SearchDTO model, MovieCategoryDTO Search)
        {
            var data = new Pagedata<MovieCategoryDTO>();
            var query = TableNoTracking.Where(c => c.IsActive);
            data.Resualt = await query.Select(c => new MovieCategoryDTO
            {
                Id = c.Id,
                Title = c.Title,
                Image=c.Image,
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
