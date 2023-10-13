using Data.DTO.BaseDTO; 
 
using Data.DTO.Movies;
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Movie
{
    public interface IMovieCategoryRepository : IRepository<Entites.Entities.Movies.MovieCategory>
    {
        Task<Pagedata<MovieCategoryDTO>> GetPaging(SearchDTO model, MovieCategoryDTO Search);
    }
}