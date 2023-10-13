using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entites.Entities;
using static Common.AllEnum.Commons;
using Data.DTO.Sales;

namespace Data.DTO.ProductReference
{
    public class ProductReferenceDTO 
    {
        public ProductReferenceDTO()
        {
            ListProductReferenceItem = new List<ProductReferenceItemDTO>();
        }
        public int OrderId { get; set; }
        public int AddressId { get; set; }
        public string Memo { get; set; }
        //public RequestState State { get; set; }
        public string Reason { get; set; }
        public List<ProductReferenceItemDTO> ListProductReferenceItem { get; set; }
    }
}
