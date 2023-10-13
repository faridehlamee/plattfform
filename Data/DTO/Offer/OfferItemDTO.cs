using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.Offer
{
    public class OfferItemDTO : BaseDto<OfferItemDTO, OfferItem, int>
    {
       
  

        public int? OfferId { get; set; }
        public virtual OfferDTO Offer { get; set; }

        public int? ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        //public List<OfferItemDTO> ListOfferItem { get; set; }

        public string ProductIds { get; set; }
    }
}
