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
using Data.DTO.BaseDTO;
using Data.DTO.Movies;
using Data.DTO.Product;

namespace Data.Repositories.Movie
{
    public class MovieRepository : Repository<Entites.Entities.Movies.Movie>, IMovieRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public MovieRepository(KiatechDbContext dbContext, IMapper Mapper, IHttpContextAccessor contextAccessor)
        : base(dbContext , contextAccessor)
        {
            _mapper = Mapper;
        }
        public async Task<Pagedata<MovieDTO>> GetPaging(SearchDTO model, MovieDTO Search)
        {
            var data = new Pagedata<MovieDTO>();

            var query = TableNoTracking.Where(c => c.IsActive);
            data.Resualt = await query
                .Select(c => new MovieDTO
                {
                    Id = c.Id,
                    MovieCategoryTitle=c.MovieCategory.Title,
                    Name =c.Name,
                    DateInsert = c.DateInsert
                }).OrderBy(e => e.DateInsert).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();

            double total = await query.CountAsync();
            data.CurrentPage = model.page;
            data.TotalPages = (int)Math.Ceiling(total / model.take); //
            data.TotalProduct = (int)total;
            return data;
        }


    }
}
