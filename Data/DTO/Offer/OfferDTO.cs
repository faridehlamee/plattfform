using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Offer
{
    public class OfferDTO : BaseDto<OfferDTO,Entites.Entities.Offer.Offer, int>
    {
        public OfferDTO()
        {
            ExpirDate = DateTime.Now.AddMonths(6);
        }
        public string Name { get; set; }
        public string description { get; set; }
        public string Image { get; set; }
        public DateTime ExpirDate { get; set; }
        public string PersianExpirDate { get; set; }

        public int OfferTypeId { get; set; }
        public OfferTypeDTO OfferType { get; set; }
        public List<SelectListItem> ListOfferType { get; set; }

        public int OfferZoneId { get; set; }
        public OfferZoneDTO OfferZone { get; set; }
        public List<SelectListItem> ListOfferZone { get; set; }

        public List<BaseProductDTO> ListProduct { get; set; }
    }
}
