using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.Discount
{
    public class DiscountDTO : BaseDto<DiscountDTO, Entites.Entities.Discount.Discount, int>
    {
        public DiscountDTO()
        {
            ExpireDate = DateTime.Now.AddMonths(6);
        }
        public DiscountType DiscountType { get; set; }
        public TypeOffPrice? TypeOffPrice { get; set; }
        //public Product.ProductDTO Product { get; set; }
        public int? ProductId { get; set; }
        public string ProductName { get; set; }
        public double? Value { get; set; }
        public string KeyDiscountPercent { get; set; }
        public bool? ForAll { get; set; }
        public int? PermittedNumber { get; set; }



        public DateTime? StartDate { get; set; }
        public string PersianStartDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string PersianExpireDate { get; set; }


        public int[] ProductIds { get; set; }
    }
}
