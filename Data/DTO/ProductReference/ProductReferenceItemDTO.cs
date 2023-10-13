using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Data.DTO.ProductReference
{
    public class ProductReferenceItemDTO 
    {
        public bool IsSelected { get; set; }
        public int OrderDetailId { get; set; }
        public double Value { get; set; }
        public ProductReferenceReason Reason { get; set; }
        public int ProductReferenceId { get; set; }
        //public virtual ProductReferenceDTO ProductReference { get; set; }
        //public int ProductId { get; set; }
        //public virtual ProductDTO Product { get; set; }

    }
}
