using Common;
using Common.Utilities;
using Data.Contracts;
using Data.Contracts.Order;
using Data.Contracts.User;
using Data.DTO;
using Entites.Entities;
using Entites.Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.ViewModel;
using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Report
{
    public class ReportService : IReportService, IScopedDependency
    {
        private readonly IRepository<StoreType> _storeTypeRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IUserRepository _userRepository;

        public ReportService(IRepository<StoreType> storeTypeRepository, IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository  , IUserRepository userRepository)
        {
            _storeTypeRepository = storeTypeRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _userRepository = userRepository;
        }

        public async Task<List<ReportModelView>> SaleOrderReport()
        {

            var lstData = new List<ReportModelView>();
            var storeType = await _storeTypeRepository.TableNoTracking.Where(c => c.IsActive).ToListAsync();

            foreach (var item in storeType)
            {
                var query = _orderDetailRepository.TableNoTracking.Where(c => c.Order.FactorType == Common.AllEnum.Commons.FactorType.sale && c.IsActive && c.Order.IsFinaly && c.Product.StoreTypeId == item.Id);
                
                var data = new ReportModelView()
                {
                    Title = item.StoreName,
                    Count = await query.CountAsync(),
                    SumPrice = await query.SumAsync(c => (c.Price * c.Value))
                };
                lstData.Add(data);
            }

            return lstData;
          
        }
        public async Task<ReportModelView> AllPriceReport()
        {
            var query = _orderRepository.TableNoTracking.Where(c => c.IsActive && (int)c.State == 4 && c.IsFinaly);
            var data = new ReportModelView()
            {
                Title = "فروش کل",
                SumPrice = await query.SumAsync(c => c.FinalPayment)
            };
            return data;
        }
        public async Task<ReportModelView> NewOrderReport()
        {
            var query = _orderRepository.TableNoTracking.Where(c => c.IsActive && c.IsFinaly && c.State==Common.AllEnum.Commons.SaleState.Pending);
            var data = new ReportModelView()
            {
                Title = "سفارش های جدید",
                Count = await query.CountAsync()
            };
            return data;
        }
        public async Task<ReportModelView> AllOrderReport()
        {
            var query = _orderRepository.TableNoTracking.Where(c => c.IsActive && (int)c.State==4 &&c.IsFinaly);
            var data = new ReportModelView()
            {
                Title = "سفارش ها ",
                Count = await query.CountAsync()
            };
            return data;
        }
        public async Task<ReportModelView> RegisterReport()
        {
            var query = _userRepository.TableNoTracking.Where(c => c.IsActive && (int)c.State >= 2);
            var data = new ReportModelView()
            {
                Title = "آمار ثبت نام",
                Count = await query.CountAsync()
            };
            return data;
        }

    }

}
     
