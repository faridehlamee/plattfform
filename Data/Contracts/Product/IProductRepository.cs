using Common.Utilities;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace Data.Contracts.Product
{
    public interface IProductRepository : IRepository<Entites.Entities.Product.Product>
    {
        Task<List<ProductDTO>> GetAll();
        Task<ProductDTO> GetDetail(int id);
        Task<List<ProductDTO>> GetbyStoreTypeId(int storeTypeId);
        Task<Pagedata<BaseProductDTO>> GetPaging(SearchDTO model, ProductDTO Search);
        Task<IQueryable<Entites.Entities.Product.Product>> Filter(SearchDTO model, IQueryable<Entites.Entities.Product.Product> query, ProductDTO Search);
    }
}
