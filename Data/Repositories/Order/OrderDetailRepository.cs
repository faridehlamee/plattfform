using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Common.Exceptions;
using Common.Utilities;
using Data.Contracts.BaseProduct;
using Data.Contracts.Discount;
using Data.Contracts.Financial;
using Data.Contracts.OfferItem;
using Data.Contracts.Order;
using Data.DTO.Cart;
using Data.DTO.ProductReference;
using Data.DTO.Sales;
using Data.Repositories.OfferItem;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Order
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly IPriceRepository _priceRepository;
        private readonly IDiscountRepository _discountRepository;

        public OrderDetailRepository(RoyalCanyonDBContext dbContext, IMapper mapper, IDiscountRepository discountRepository,
            IPriceRepository priceRepository, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = mapper;
            _priceRepository = priceRepository;
            _discountRepository = discountRepository;
        }

        public async Task<List<OrderDetailDTO>> GetDetailbyOrderId(int orderId)
        {
            var data = await TableNoTracking.Where(c => c.OrderId == orderId)
                .ProjectTo<OrderDetailDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return data;
        }
        public async Task<bool> AddOrderDetails(int OrderId, List<CartDTO> cartDTOs, CancellationToken cancellationToken)
        {

            var listDetails = new List<OrderDetail>();
            foreach (var item in cartDTOs)
            {
                var mainprice = await _priceRepository.GetPrice(item.ProductId);
                var discount = await _discountRepository.GetByProductId(item.ProductId);

                var details = new OrderDetail()
                {
                    ProductId = item.ProductId,
                    MainPriceId = mainprice.Id,
                    Value = item.Value,
                    OrderId = OrderId,
                    ProductWareHouseId = item.ProductWareHouseId,
                    Price = item.FinalAmount,
                };

                if (discount != null)
                    details.DiscountId = discount.Id;

                listDetails.Add(details);
            }


            await AddRangeAsync(listDetails, cancellationToken);

            return true;
        }

        public async Task<double> CreateProductReferenceItem(List<ProductReferenceItemDTO> ListProductReferenceItem, int ProductReferenceId, CancellationToken cancellationToken)
        {
            var detailIds = ListProductReferenceItem.Select(c => c.OrderDetailId);
            var listdetail = await TableNoTracking.Where(c => detailIds.Contains(c.Id)).ToListAsync();
            var newLisctdetails = new List<OrderDetail>();
            foreach (var item in listdetail)
            {
                var newProductReferenceItem = ListProductReferenceItem.Where(c => c.OrderDetailId == item.Id).FirstOrDefault();
                if (newProductReferenceItem.Value <= item.Value)
                {
                    var newitem = new OrderDetail()
                    {
                        Value = newProductReferenceItem.Value,
                        Reason = newProductReferenceItem.Reason.ToDisplay(),
                        OrderId = ProductReferenceId,
                        Price = item.Price,
                        MainPriceId = item.MainPriceId,
                        DiscountId = item.DiscountId,
                        ProductId = item.ProductId,
                        ProductWareHouseId = item.ProductWareHouseId,
                        IsActive = item.IsActive,
                    };

                    newLisctdetails.Add(newitem);
                }
            }

            await AddRangeAsync(newLisctdetails, cancellationToken);
            return newLisctdetails.Sum(c => (c.Price * c.Value));
        }
    }
}
