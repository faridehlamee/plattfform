using Data.DTO.Blog; 
using Data.DTO.Common;
using Data.DTO.Offer;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ViewModel
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            Portfolios = new List<PortfolioDTO>();
        }
        public List<OfferDTO> BannerUnderSlider { get; set; }
        public List<OfferDTO> RightBanner { get; set; }
        public List<SliderDTO> Sliders { get; set; }
        public List<ProductDTO> productID { get; set; }


        public List<BlogDTO> LastArticles { get; set; }
        public List<BlogDTO> LastBlog { get; set; }
        public List<PortfolioDTO> Portfolios { get; set; } 
        public List<ServiceDTO>  Services { get; set; }
        public List<TeamDTO>   Teams { get; set; }
        public List<PricingDTO> Pricings { get; set; }

        //public List<BaseProductDTO> SportsSupplement { get; set; }
        //public List<BaseProductDTO> JoggingSuit { get; set; }
        //public List<BaseProductDTO> ClubEquipment { get; set; }


    }
}
