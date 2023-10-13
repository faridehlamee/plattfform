using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Entites.Entities.WareHouse;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums.Product;

namespace Data.DTO.WareHouse
{
    public class ProductWareHouseDTO : BaseDto<ProductWareHouseDTO, ProductWareHouse, int>
    {
        public double value { get; set; }

        public int ProductId { get; set; }

        //public virtual ProductDTO Product { get; set; }
        public TypeWarehouse TypeWarehouse { get; set; }
        public int TypeSizeItemId { get; set; }
        public string TypeSizeItemName { get; set; }

        //public virtual TypeSizeItemDTO TypeSizeItem { get; set; }


        public List<SelectListItem> ListTypeItem { get; set; }
    }
}
