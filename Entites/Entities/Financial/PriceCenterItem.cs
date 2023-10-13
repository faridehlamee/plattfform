using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Financial
{
    [Table("PriceCenterItem", Schema = "FIN")]
    public class PriceCenterItem : BaseEntity<int>, IEntity<int>
    {
        public string Title { get; set; }
        public double Amount { get; set; }

        public int PriceCenterId { get; set; }
        public virtual PriceCenter PriceCenter { get; set; }
    }
}
