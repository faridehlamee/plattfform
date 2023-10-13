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
using Data.Contracts.Offer;
using Data.Contracts.OfferItem;
using Data.Contracts.Order;
using Data.DTO.BaseDTO;
using Data.DTO.Cart;
using Data.DTO.Product;
using Data.DTO.ProductReference;
using Data.DTO.Sales;
using Entites.Entities;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static Common.AllEnum.Commons;

namespace Data.Repositories.Order
{
    public class OrderRepository : Repository<Entites.Entities.Order>, IOrderRepository, IScopedDependency
    {

        private readonly IMapper _mapper;

        public OrderRepository(KiatechDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = mapper;
        }
        public async Task<Pagedata<OrderDTO>> GetListOrder(SearchDTO model, OrderDTO orderDTO)
        {
            var data = new Pagedata<OrderDTO>();
            var query = TableNoTracking
               .Where(c => c.IsActive);
            query = Filter(query, orderDTO);
            data.Resualt = await query.ProjectTo<OrderDTO>(_mapper.ConfigurationProvider)
                .OrderByDescending(c => c.DateInsert)
                .Skip(model.take * (model.page - 1))
                .Take(model.take).ToListAsync();
            data.TotalPages = await query.CountAsync();

            return data;
        }

        public IQueryable<Entites.Entities.Order> Filter(IQueryable<Entites.Entities.Order> query, OrderDTO orderDTO)
        {
            if (orderDTO.UserId != 0)
            {
                query = query.Where(c => c.UserId == orderDTO.UserId);
            }
            if (orderDTO.IsFinaly == false)
            {
                query = query.Where(c => c.IsFinaly == orderDTO.IsFinaly);
            }
            if (orderDTO.IsFinaly == true)
            {
                query = query.Where(c => c.IsFinaly == orderDTO.IsFinaly);
            }
            if (orderDTO.UserFirstName != null)
            {
                query = query.Where(c => c.User.FirstName.Contains(orderDTO.UserFirstName));
            }
            if (orderDTO.UserLastName != null)
            {
                query = query.Where(c => c.User.LastName.Contains(orderDTO.UserLastName));
            }
            if (orderDTO.UserPhoneNumber != null)
            {
                query = query.Where(c => c.User.PhoneNumber.Contains(orderDTO.UserPhoneNumber));
            }
            if (orderDTO.RefId != null)
            {
                query = query.Where(c => c.RefId.Contains(orderDTO.RefId));
            }

            if (orderDTO.State != 0)
            {
                query = query.Where(c => c.State == orderDTO.State);
            }

            return query;

        }

        public async Task<Entites.Entities.Order> CreateOrder(BillDTO data, int userId, double totalPayment, double finalPayment, double totalExtraAmount, CancellationToken cancellationToken)
        {
            double discount = 0;
            discount = (totalPayment - finalPayment);


            var order = new Entites.Entities.Order()
            {
                FactorType = FactorType.sale,
                UserId = userId,
                AddressId = data.AddressId,
                PaymentType = data.PaymentType,
                Index = 1,
                FacPart = DateTime.Now.GetPrsianDate(),
                IsFinaly = false,
                Memo = data.Memo,
                TotalPayment = totalPayment,
                TotalExtraAmount = totalExtraAmount,
                TotalDiscount = discount,
                FinalPayment = totalPayment + totalExtraAmount - discount,
                State = SaleState.Pending,
                Status = 0,
                RefId = "",
                StatusDes = ""


            };
            await AddAsync(order, cancellationToken);
            return order;
        }

        public async Task<OrderDTO> GetDetail(int id)
        {
            var data = await TableNoTracking.Where(c => c.Id == id)
                .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider).SingleOrDefaultAsync();
            return data;

        }

        public async Task<List<OrderDTO>> GetCurrentUserOrder(int UserId)
        {
            var query = TableNoTracking
              .Where(c => c.IsActive && c.UserId == UserId).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider);
            return await query.ToListAsync();
        }

        public async Task<int> CountCoponUsedById(int coponId, int userId)
        {
            return await TableNoTracking.Where(c => c.DiscountId == coponId && c.UserId == userId && c.IsActive && c.IsFinaly).CountAsync();
        }

        public async Task<Entites.Entities.Order> ApplyCode(int orderId, Entites.Entities.Discount.Discount discount, CancellationToken cancellationToken)
        {
            var order = await Table.Where(c => c.Id == orderId).SingleOrDefaultAsync();

            if (order.TotalDiscount != 0)
                return order;
            if (discount == null)
                return order;

            if (discount.TypeOffPrice == TypeOffPrice.amount)
            {
                order.TotalDiscount = discount.Value.Value;
                order.FinalPayment = order.TotalPayment + order.TotalExtraAmount - discount.Value.Value;
                order.DiscountId = discount.Id;
            }
            else
            {
                order.TotalDiscount = (order.TotalPayment * (discount.Value.Value / 100));
                order.FinalPayment = order.TotalPayment + order.TotalExtraAmount - (order.TotalPayment * (discount.Value.Value / 100));
                order.DiscountId = discount.Id;
            }

            await UpdateAsync(order, cancellationToken);
            return order;

        }


        public string CreateRefCode()
        {
            Random rnd = new Random();
            var num = rnd.Next(1000, 9999);
            var num2 = rnd.Next(1000, 9999);
            var num3 = rnd.Next(1000, 9999);
            return num.ToString() + num2.ToString() + num3.ToString();
        }



        public async Task<List<OrderDTO>> GetListProductReference(int UserId)
        {
            var hasrefrence = await TableNoTracking.Where(c => c.IsActive && c.UserId == UserId && (int)c.State >= 4 && c.ParentId != null)
              .Select(x => x.ParentId).ToListAsync();


            var query = TableNoTracking
              .Where(c => !hasrefrence.Contains(c.Id) && c.IsActive && c.UserId == UserId && c.ParentId == null && (int)c.State == 4 && c.DateUpdate.Value.AddDays(10) > DateTime.Now)
              .ProjectTo<OrderDTO>(_mapper.ConfigurationProvider);
            return await query.ToListAsync();
        }


        public async Task<Entites.Entities.Order> CreateProductReference(ProductReferenceDTO ProductReference, CancellationToken cancellationToken)
        {
            var order = await GetByIdAsync(cancellationToken, ProductReference.OrderId);
            var Reference = new Entites.Entities.Order()
            {
                FacPart = DateTime.Now.GetPrsianDate(),
                FactorType = FactorType.ret,
                RefId = "RE-" + CreateRefCode(),
                UserId = order.UserId,
                AddressId = order.AddressId,
                IsFinaly = true,
                Memo = $"{ProductReference.Memo} - {ProductReference.Reason} ",
                State = SaleState.ReturnedRequest,
                PaymentType = PaymentType.wallet,
                ParentId = order.Id,
                Status = 100,
                Index = 1,
                TotalDiscount = 0,
                TotalPayment = 0,
                FinalPayment = 0,
                TotalExtraAmount = order.TotalExtraAmount,
                DiscountId = order.DiscountId
            };


            await AddAsync(Reference, cancellationToken);

            return Reference;
        }


        public async Task<OrderDTO> GetOrderById(int userId, int orderId)
        {
            var query = TableNoTracking
              .Where(c => c.Id == orderId && c.UserId == userId).ProjectTo<OrderDTO>(_mapper.ConfigurationProvider);
            return await query.FirstOrDefaultAsync();
        }
    }
}
