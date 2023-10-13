using Data.DTO.BaseDTO;
using Data.DTO.Blog; 
 
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Blog
{
    public interface IBlogCategoryRepository : IRepository<Entites.Entities.BlogCategory>
    {
        Task<Pagedata<BlogCategoryDTO>> GetPaging(SearchDTO model, BlogCategoryDTO Search);

    }
}