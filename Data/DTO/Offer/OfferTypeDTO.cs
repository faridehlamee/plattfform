using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Offer
{
    public class OfferTypeDTO : BaseDto<OfferTypeDTO, OfferType, int>
    {
        public string Type { get; set; }
    }
}
