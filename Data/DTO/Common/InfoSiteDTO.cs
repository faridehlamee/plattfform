using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class InfoSiteDTO : BaseDto<InfoSiteDTO, InfoSite, int>
    {

        public InfoSiteDTO()
        {
            IsCheck = false;
        }

        public int Key { get; set; }
        public string Value { get; set; }

        public bool IsCheck { get; set; }

    }
}
