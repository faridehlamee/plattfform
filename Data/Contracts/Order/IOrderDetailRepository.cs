using Data.DTO.Cart;
using Data.DTO.ProductReference;
using Data.DTO.Sales;
using Data.Repositories;
using Entites.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Order
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        Task<List<OrderDetailDTO>> GetDetailbyOrderId(int orderId);
        Task<bool> AddOrderDetails(int OrderId, List<CartDTO> cartDTOs, CancellationToken cancellationToken);
        Task<double> CreateProductReferenceItem(List<ProductReferenceItemDTO> ListProductReferenceItem, int ProductReferenceId, CancellationToken cancellationToken);




    }
}