using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Address
{
    public class ProvinceDTO : BaseDto<ProvinceDTO, Province, int>
    {
        public string Name { get; set; }
    }
}
