using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities.Discount
{
    public class Discount : BaseEntity<int>, IEntity<int>
    {
        public DiscountType DiscountType { get; set; }
        public TypeOffPrice? TypeOffPrice { get; set; }
        public Product.Product Product { get; set; }
        public int? ProductId { get; set; }
        public double? Value { get; set; }
        public string KeyDiscountPercent { get; set; }
        public bool? ForAll { get; set; }
        public int? PermittedNumber { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
