using Data.DTO.BaseDTO;
using Entities.Entities.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.Enums.Product;

namespace Data.DTO.WareHouse
{
    public class TypeSizeDTO : BaseDto<TypeSizeDTO, TypeSize, int>
    {
        public string Name { get; set; }
        public TypeWarehouse TypeWarehouse { get; set; }
    }
}
