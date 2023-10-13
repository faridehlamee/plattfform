using Data.DTO.BaseDTO;
using Data.DTO.Common; 
using Data.DTO.Product;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.Contracts.Common
{
    public interface IEmailRepository : IRepository<Entites.Entities.Email>
    {
        Task<Pagedata<EmailDTO>> GetPaging(SearchDTO model, EmailDTO Search);
 
    }
}