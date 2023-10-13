using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class GenderProductTypeDTO : BaseDto<GenderProductTypeDTO, GenderProductType, int>
    {
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
