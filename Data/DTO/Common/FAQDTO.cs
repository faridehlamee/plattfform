using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class FAQDTO : BaseDto<FAQDTO, FAQ, int>
    {
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}
