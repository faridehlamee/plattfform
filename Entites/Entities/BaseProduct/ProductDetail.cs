using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("ProductDetail", Schema = "BPR")]
    public class ProductDetail : BaseEntity<int>, IEntity<int>
    {
        public int ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

        public int DetailsItemId { get; set; }
        public virtual DetailsItem DetailsItem { get; set; }

    }
}
 