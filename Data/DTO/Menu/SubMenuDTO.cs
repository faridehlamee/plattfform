using Data.DTO.BaseDTO;
using Entites.Entities.Menu;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Menu
{
    public class SubMenuDTO : BaseDto<SubMenuDTO, SubMenu, int>
    {
        public string SubTitle { get; set; }
        public int Level { get; set; }

        public string URL { get; set; }

        public int MenuId { get; set; }
        public MenuDTO Menu { get; set; }

        public int? ParentId { get; set; }
        public SubMenuDTO Parent { get; set; }

        public List<SelectListItem> MenuList { get; set; }
        public List<SelectListItem> ListSubMenu { get; set; }
    }
}
