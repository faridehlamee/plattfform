using Data.DTO.BaseDTO; 
 
using Data.DTO.Movies;
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Movie
{
    public interface IMovieRepository : IRepository<Entites.Entities.Movies.Movie>
    {
        Task<Pagedata<MovieDTO>> GetPaging(SearchDTO model, MovieDTO Search);
    }
}