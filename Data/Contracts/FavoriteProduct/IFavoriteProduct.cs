using Data.DTO.BaseDTO; 
using Data.DTO.FavoriteProductDTO;
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.FavoriteProduct
{
    public interface IFavoriteProduct : IRepository<Entites.Entities.FavoriteProduct.FavoriteProduct>
    {
       

    }
}
