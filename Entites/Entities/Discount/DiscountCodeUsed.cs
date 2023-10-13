using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Discount
{
    public class DiscountCodeUsed : BaseEntity<int>, IEntity<int>
    {
        public int DiscountId { get; set; }
        public Discount Discount { get; set; }

        public int UserId { get; set; }
        public User.User User { get; set; }

        public bool IsCompleted { get; set; }

    }
}
