using Data.DTO.BaseDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Product
{
    
    public class SearchDTO : BasePaging
    {
        public string Search { get; set; }
        public string Filters { get; set; }
        //  public List<long> Categories { get; set; }

        public ProductOrderBy? OrderBy { get; set; }
        public int? OfferId { get; set; }
        public List<int> D { get; set; }
        public List<FilterDTO> Filter { get; set; }
        public int? CreatorId { get; set; }
        public int? StoreTypeId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? BrandId { get; set; }
        public int? MaterialId { get; set; }
        public bool? IsShow { get; set; }

    }
    public class SearchDTO<T>: SearchDTO
    {
        public T filter { get; set; }
    }
    public enum ProductOrderBy
    {
        PriceAsc,
        PriceDec
    }
}
