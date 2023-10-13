using Data.DTO.Cart;
using Data.Repositories;
using Entites.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Cart
{
    public interface ICartRepository : IRepository<Entites.Entities.Cart.Cart>
    {
        Task<string> AddToCart(InsertCartDTO model);
        string getKey();
        Task<List<CartDTO>> GetCart();
        void ResetCart();
    }
}