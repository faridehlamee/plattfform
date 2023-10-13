using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.User;
using Entites.Entities.User;
using System.Threading.Tasks;

namespace Data.Contracts.User
{
    public interface IIdentificationCodeRuleRepository : IRepository<IdentificationCodeRule>
    {
        Task<Pagedata<IdentificationCodeRuleDTO>> GetList(SearchDTO model, IdentificationCodeRuleDTO filter);
        Task<ResultDTO<IdentificationCodeRule>> GetRule();
    }
}