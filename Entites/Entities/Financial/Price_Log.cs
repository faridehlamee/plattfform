using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Price_Log", Schema = "FIN")]
    public class Price_Log : BaseEntity<int>, IEntity<int>
    {
        public double Amount { get; set; }
        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }
    }
}
