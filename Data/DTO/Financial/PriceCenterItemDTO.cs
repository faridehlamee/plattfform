using Data.DTO.BaseDTO;
using Entites.Entities.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Financial
{
    public class PriceCenterItemDTO : BaseDto<PriceCenterItemDTO, PriceCenterItem, int>
    {
        public string Title { get; set; }
        public double Amount { get; set; }

        public int PriceCenterId { get; set; }
        //public virtual PriceCenterDTO PriceCenter { get; set; }
    }
}
