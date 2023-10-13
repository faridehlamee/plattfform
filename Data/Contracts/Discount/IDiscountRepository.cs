using Data.DTO.BaseDTO;
using Data.DTO.Discount;
using Data.DTO.Product;
using Entites.Entities.Discount;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.Discount
{
    public interface IDiscountRepository : IRepository<Entites.Entities.Discount.Discount>
    {
        Task<Pagedata<DiscountDTO>> GetList(SearchDTO model, DiscountDTO filter);
        Task<int> GetNumberAllow(int id);
        ValueTask<Entites.Entities.Discount.Discount> CheckCopon(string copon, int UserId);
        Task<DiscountDTO> GetByProductId(int ProductId);
        Task<List<DiscountDTO>> GetDiscountCodeList();
        Task IsExistThenRemove(int productId, CancellationToken cancellationToken);
    }

}