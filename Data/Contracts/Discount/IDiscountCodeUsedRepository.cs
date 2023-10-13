using Data.DTO.BaseDTO;
using Data.DTO.Discount;
using Data.DTO.Product;
using Entites.Entities.Discount;
using System.Threading.Tasks;

namespace Data.Contracts.Discount
{
    public interface IDiscountCodeUsedRepository : IRepository<DiscountCodeUsed>
    {
        Task<Pagedata<DiscountCodeUsedDTO>> GetList(SearchDTO model, DiscountCodeUsedDTO filter);

        Task<Entites.Entities.Discount.DiscountCodeUsed> GetUserCode(int userId, int DiscountId);
    }
}