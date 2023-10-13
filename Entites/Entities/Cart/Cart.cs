using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Cart
{
    [Table("Cart", Schema = "SAL")]
    public class Cart :  IEntity<int>
    {
        public Cart()
        {
            IsActive = true;
            DateInsert = DateTime.Now;
        }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateUpdate { get; set; }
        public long CreatorId { get; set; }

        public string key { get; set; }

        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }
        public double FinalAmount { get; set; }

        public int ProductWareHouseId { get; set; }
        public virtual WareHouse.ProductWareHouse ProductWareHouse { get; set; }

        public double Value { get; set; }
    }
}
