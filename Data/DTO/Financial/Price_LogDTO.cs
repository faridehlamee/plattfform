using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Financial
{
    public class Price_LogDTO : BaseDto<Price_LogDTO, Price_Log, int>
    {
        public double Amount { get; set; }
        public int ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
    }
}
