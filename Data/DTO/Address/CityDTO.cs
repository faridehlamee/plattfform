using Data.DTO.BaseDTO;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Address
{
    public class CityDTO : BaseDto<CityDTO, City, int>
    {
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public virtual ProvinceDTO Province { get; set; }

        public List<SelectListItem> ListProvince { get; set; }
    }
}
