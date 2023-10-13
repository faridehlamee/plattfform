using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Entites.Entities
{
    [Table("OrderDetail", Schema = "SAL")]
    public class OrderDetail : BaseEntity<int>, IEntity<int>
    {
        public double Price { get; set; }
        public double Value { get; set; }

        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

        public int ProductWareHouseId { get; set; }
        public virtual WareHouse.ProductWareHouse ProductWareHouse { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int? DiscountId { get; set; }
        public virtual Discount.Discount Discount { get; set; }

        public int? MainPriceId { get; set; }
        public virtual Price_Log MainPrice { get; set; }
        public string Reason { get; set; }
    }
}
