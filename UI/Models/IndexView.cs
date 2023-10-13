using Data.DTO.Common;
using Data.DTO.Offer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class IndexView
    {
        public List<SliderDTO> Slider { get; set; }

        public List<OfferDTO> Offer { get; set; }


    }
}
