using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Entites.Entities;
using Entites.Entities.WareHouse;
using Data.Contracts.WareHouse;
using Data.DTO.WareHouse;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using Data.DTO.Sales;
using Data.DTO.BaseDTO;
using Microsoft.Extensions.Logging;

namespace Data.Repositories.WareHouse
{
    public class ProductWareHouseRepository : Repository<ProductWareHouse>, IProductWareHouseRepository, IScopedDependency
    {
        private readonly IMapper _mapper;
        private readonly ILogger<ProductWareHouseRepository> _logger;

        public ProductWareHouseRepository(IMapper Mapper , ILogger<ProductWareHouseRepository> logger,  RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
        : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
            _logger = logger;
        }
      
        public async Task<bool> CheckInventory(int WareHouseId  , double value)
        {
            var data = await TableNoTracking.AnyAsync(c => c.IsActive && c.Id == WareHouseId && c.value >= value);
            return data;
        }
        public async Task<List<ProductWareHouseDTO>> GetbyProductId(int ProductId)
        {
         //return await TableNoTracking.Where( c=> c.IsActive && c.ProductId == ProductId).ProjectTo<ProductWareHouseDTO>(_mapper.ConfigurationProvider).ToListAsync();
          return await TableNoTracking.Where( c=> c.IsActive && c.ProductId == ProductId).Select(m=> new ProductWareHouseDTO { 
          Id = m.Id,
          TypeSizeItemId = m.TypeSizeItemId,
          TypeSizeItemName = m.TypeSizeItem.Name,
          value=m.value,
          TypeWarehouse = m.TypeSizeItem.TypeSize.TypeWarehouse

          }).OrderBy(x=> x.TypeSizeItemId).ToListAsync();
        }


        public async Task<bool> UpdateWareHouseAfterSale(List<OrderDetailDTO> orderDetails, CancellationToken cancellationToken)
        {
            try
            {
                //var listWareHouse = new List<ProductWareHouse>();
                foreach (var item in orderDetails)
                {
                    var data = new ProductWareHouse();
                    data = await GetByIdAsync(cancellationToken, item.ProductWareHouseId);
                    var oldvalue = data.value;
                    data.value = data.value - item.Value;
                    //listWareHouse.Add(data);
                  await UpdateAsync(data, cancellationToken);
                  _logger.LogError($"productId={item.ProductId} warehouseId={data.Id} oldValue={oldvalue} selectedValue={item.Value}  newValue={data.value} orderId={item.OrderId}");
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<ResultDTO> LastCheckBeforPayment(List<OrderDetailDTO> orderDetails, CancellationToken cancellationToken)
        {
            var res = new ResultDTO();
            try
            {
                foreach (var item in orderDetails)
                {
                    var data = await GetByIdAsync(cancellationToken, item.ProductWareHouseId);
                    if (data.value < item.Value)
                    {
                        _logger.LogError("متاسفانه محصول در لحظه فروش رفت");
                        res.Messages = "متاسفانه محصول در لحظه فروش رفت";
                        res.Status = false;
                        return res;
                    }
                }
               
                res.Status = true;
                return res;

                
            }
            catch (Exception)
            {
                res.Messages = "خطا- بررسی موجودی";
                res.Status = false;
                return res;
            }
        }
    }
}
