using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.ProductReference;
using Data.DTO.Sales;
using Data.Repositories;
using Entites.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Order
{
    public interface IOrderRepository : IRepository<Entites.Entities.Order>
    {

        Task<Pagedata<OrderDTO>> GetListOrder(SearchDTO model, OrderDTO orderDTO);
        Task<List<OrderDTO>> GetCurrentUserOrder(int UserId);
        Task<Entites.Entities.Order> CreateOrder(BillDTO data, int userId, double totalPayment, double finalPayment, double totalExtraAmount, CancellationToken cancellationToken);
        Task<OrderDTO> GetDetail(int id);
        Task<int> CountCoponUsedById(int coponId, int userId);

        Task<Entites.Entities.Order> ApplyCode(int orderId, Entites.Entities.Discount.Discount discount, CancellationToken cancellationToken);
        string CreateRefCode();
        Task<Entites.Entities.Order> CreateProductReference(ProductReferenceDTO ProductReference, CancellationToken cancellationToken);

        Task<OrderDTO> GetOrderById(int userId, int orderId);
        Task<List<OrderDTO>> GetListProductReference(int UserId);
    }
}