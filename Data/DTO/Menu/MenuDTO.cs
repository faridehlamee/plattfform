using Data.DTO.BaseDTO;
using Data.DTO.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Menu
{
    public class MenuDTO : BaseDto<MenuDTO, Entites.Entities.Menu.Menu, int>
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public string URL { get; set; }

        public int Level { get; set; }

        public int? ParentId { get; set; }
        public MenuDTO Parent { get; set; }
        public ICollection<SubMenuDTO> CltSubMenu { get; set; }
    }
}

