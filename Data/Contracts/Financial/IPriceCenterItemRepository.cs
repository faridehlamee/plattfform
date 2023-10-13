using Data.DTO.Financial;
using Data.Repositories;
using Entites.Entities.Financial;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Financial
{
    public interface IPriceCenterItemRepository : IRepository<PriceCenterItem>
    {

        Task<List<PriceCenterItemDTO>> GetByPriceCenterTitle(string Code);
    }
}