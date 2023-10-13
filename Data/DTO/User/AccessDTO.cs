using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.User
{
    public class AccessDTO : BaseDto<AccessDTO, Access, int>
    {
        public string Title { get; set; }
    }
}
