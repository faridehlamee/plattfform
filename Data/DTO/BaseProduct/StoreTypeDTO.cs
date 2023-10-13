using Data.DTO.BaseDTO;
using Data.DTO.WareHouse;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class StoreTypeDTO : BaseDto<StoreTypeDTO, StoreType, int>
    {
        public string StoreName { get; set; }
        public string Image { get; set; }
        

        public int GenderProductTypeId { get; set; }
        public virtual GenderProductTypeDTO GenderProductType { get; set; }
        public List<SelectListItem> GenderProductTypeList { get; set; }

        //public int TypeSizeId { get; set; }
        //public virtual TypeSizeDTO TypeSize { get; set; }
        //public List<SelectListItem> TypeSizeList { get; set; }

    }
}
