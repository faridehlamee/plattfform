using Data.DTO.BaseDTO;
using Data.DTO.Blog;
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Blog
{
    public interface IBlogRepository : IRepository<Entites.Entities.Blog>
    {
        Task<Pagedata<BlogDTO>> GetPaging(SearchDTO model, BlogDTO Search);

    }
}