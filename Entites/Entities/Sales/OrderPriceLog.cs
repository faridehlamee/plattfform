using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities
{
    [Table("OrderPriceLog", Schema = "SAL")]
    public class OrderPriceLog : BaseEntity<int>, IEntity<int>
    {

        public int OrderId { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; }

    }
}
