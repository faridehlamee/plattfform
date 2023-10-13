using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Offer
{
    public class OfferZoneDTO : BaseDto<OfferZoneDTO, OfferZone, int>
    {
        public string Name { get; set; }
    }
}
