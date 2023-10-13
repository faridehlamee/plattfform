using Data.DTO.BaseDTO;
using Entites.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.User
{
    public class RoleDTO : BaseDto<RoleDTO, Role, int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
