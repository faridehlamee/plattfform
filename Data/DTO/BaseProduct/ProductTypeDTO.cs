using Data.DTO.BaseDTO;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class ProductTypeDTO : BaseDto<ProductTypeDTO, ProductType, int>
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public int StoreTypeId { get; set; }
        public virtual StoreTypeDTO StoreType { get; set; }
        public List<SelectListItem> StoreTypeList { get; set; }
    }
}
