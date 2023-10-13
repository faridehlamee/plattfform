using Data.DTO.BaseDTO;
using Entities.Entities.WareHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.WareHouse
{
    public class TypeSizeItemDTO : BaseDto<TypeSizeItemDTO, TypeSizeItem, int>
    {
        public string Name { get; set; }
        public int TypeSizeId { get; set; }
        public virtual TypeSizeDTO TypeSize { get; set; }

        public List<SelectListItem> DetailsList { get; set; }
    }
}
