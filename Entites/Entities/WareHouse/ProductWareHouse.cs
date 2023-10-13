using Entities.Common;
using Entities.Entities.WareHouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.WareHouse
{
    [Table("ProductWareHouse", Schema = "PW")]
    public class ProductWareHouse : BaseEntity<int>, IEntity<int>
    {
        public double value { get; set; }

        public int ProductId { get; set; }

        public virtual Product.Product Product { get; set; }

        public int TypeSizeItemId { get; set; }

        public virtual TypeSizeItem TypeSizeItem { get; set; }
    }
}
