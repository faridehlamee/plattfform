using Data.DTO.BaseDTO;
using Data.DTO.BaseProduct;
using Data.DTO.Product;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.BaseProduct
{
    public interface IProductDetailRepository : IRepository<ProductDetail>
    {

        ProductDetailDTO GetbyDetaiIdAndProductId(int detailId, int ProductId);
        ProductDetailDTO GetbyDetaiItemIdAndProductId(int detailItemId, int ProductId);
        Task<List<FilterDTO>> GetFilter(int[] Productids);
        Task<List<ProductDetailDTO>> GetByProductId(int productid);
    }
}