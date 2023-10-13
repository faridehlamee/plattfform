using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Recharge;
using Data.Repositories;
using Entites.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Recharge
{
    public interface IRechargeRepository : IRepository<Entites.Entities.Recharge>
    {
        Task<bool> AddToReChatge(RechargeDTO model, CancellationToken cancellationToken);
        Task<Pagedata<RechargeDTO>> GetListRecharge(SearchDTO model, RechargeDTO rechargeDTO);
        Task<List<Entites.Entities.Recharge>> UpdateInventoryAnnouncement(CancellationToken cancellationToken);
    }
}