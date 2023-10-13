using Data.DTO.BaseDTO;
using Data.DTO.Financial;
using Data.DTO.Offer;
using Data.DTO.Product;
using Data.DTO.WareHouse;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.Sales
{
    public class OrderDetailDTO : BaseDto<OrderDetailDTO, OrderDetail, int>
    {
        public double Price { get; set; }
        public double Value { get; set; }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCurrentImage { get; set; }
        //public virtual ProductDTO Product { get; set; }

        public int ProductWareHouseId { get; set; }
        public string ProductWareHouseTypeSizeItemName { get; set; }
        //public virtual ProductWareHouseDTO ProductWareHouse { get; set; }

        public int OrderId { get; set; }
        //public virtual OrderDTO Order { get; set; }

        public int? OfferItemId { get; set; }
        //public virtual OfferItemDTO OfferItem { get; set; }

        public int? MainPriceId { get; set; }
        public double MainPriceAmount { get; set; }
        //public virtual Price_LogDTO MainPrice { get; set; }
        public string Reason { get; set; }

        public bool IsSelected { get; set; }
        public ProductReferenceReason ReferenceReason { get; set; }

    }
}
