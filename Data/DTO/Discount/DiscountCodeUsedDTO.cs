using Data.DTO.BaseDTO;
using Data.DTO.User;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Discount
{
    public class DiscountCodeUsedDTO : BaseDto<DiscountCodeUsedDTO, Entites.Entities.Discount.DiscountCodeUsed, int>
    {
        public int DiscountId { get; set; }
        public DiscountDTO Discount { get; set; }

        public int UserId { get; set; }
        public UserDTO User { get; set; }

        public bool IsCompleted { get; set; }
    }
}
