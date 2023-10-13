using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class TeamDTO : BaseDto<TeamDTO, Team, int>
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string JobDescription { get; set; }
        public string Bio { get; set; }
        public string LinkedIdUrl { get; set; }
    }
}
