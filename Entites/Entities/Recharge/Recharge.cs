using Entites.Entities.WareHouse;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Recharge", Schema = "PRO")]
    public class Recharge : BaseEntity<int> , IEntity<int>
    {
        public Recharge()
        {
            IsCheck = false;
        }
        public int ProductWareHouseId { get; set; }
        public ProductWareHouse ProductWareHouse { get; set; }
        public string PhonNumber { get; set; }
        public bool IsCheck { get; set; }
    }
}
