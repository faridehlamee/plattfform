
using Data.DTO.BaseDTO;
using Data.DTO.Sales;
using Data.DTO.WareHouse;
using Data.Repositories;
using Entites.Entities;
using Entites.Entities.WareHouse;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Data.Contracts.WareHouse
{
    public interface IProductWareHouseRepository : IRepository<ProductWareHouse>
    {
        Task<bool> CheckInventory(int WareHouseId, double value);
        Task<List<ProductWareHouseDTO>> GetbyProductId(int ProductId);
        Task<bool> UpdateWareHouseAfterSale(List<OrderDetailDTO> orderDetails, CancellationToken cancellationToken);
        Task<ResultDTO> LastCheckBeforPayment(List<OrderDetailDTO> orderDetails, CancellationToken cancellationToken);
    }
}