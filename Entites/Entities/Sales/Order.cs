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
    [Table("Order", Schema = "SAL")]
    public class Order : BaseEntity<int>, IEntity<int>
    {
        public string FacPart { get; set; }
        public FactorType FactorType { get; set; }
        public string RefId { get; set; }
        public int UserId { get; set; }
        public virtual User.User User { get; set; }
        public int? AddressId { get; set; }
        public virtual Address Address { get; set; }
        public PaymentType? PaymentType { get; set; }
        public bool IsFinaly { get; set; }


        public double TotalDiscount { get; set; }
        public double TotalExtraAmount { get; set; }
        public double TotalPayment { get; set; }
        public double FinalPayment { get; set; }


        public string Memo { get; set; }
        public int Index { get; set; }
        public int Status { get; set; }
        public string StatusDes { get; set; }

        public SaleState State { get; set; }
        public bool? IsProductReference { get; set; }
        public float? newPrice { get; set; }
        public int? ParentId { get; set; }

        public int? DiscountId { get; set; }
        public Discount.Discount Discount { get; set; }
        //public ICollection<OrderDetail> clcOrderDetail { get; set; }

    }
}
