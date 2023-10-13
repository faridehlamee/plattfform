using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class ServiceDTO : BaseDto<ServiceDTO, Service, int>
    {
        public string Icone { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }
    }
}
