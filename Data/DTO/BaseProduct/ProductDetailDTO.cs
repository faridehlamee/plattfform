using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class ProductDetailDTO : BaseDto<ProductDetailDTO, ProductDetail, int>
    {
        public int ProductId { get; set; }
        //public virtual ProductDTO Product { get; set; }

        public int DetailsItemId { get; set; }
        public string DetailsItemSubTopic { get; set; }
        public int DetailsItemDetailsId { get; set; }
        public string DetailsItemDetailsTitle { get; set; }
        
        //public virtual DetailsItemDTO DetailsItem { get; set; }


        public int DetailId { get; set; }
        public string DetailTitle { get; set; }
        
        //public virtual DetailsDTO Detail { get; set; }

        public List<SelectListItem> ListDetailItem { get; set; }
    }
}
