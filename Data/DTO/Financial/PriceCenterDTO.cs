using Data.DTO.BaseDTO;
using Entites.Entities.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Financial
{
    public class PriceCenterDTO : BaseDto<PriceCenterDTO, PriceCenter, int>
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
