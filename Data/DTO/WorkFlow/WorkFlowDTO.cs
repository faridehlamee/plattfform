using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.WorkFlow
{
    public class WorkFlowDTO : BaseDto<WorkFlowDTO, Entites.Entities.WorkFlow.WorkFlow, int>
    {
        public string EntityType { get; set; }
        public int Index { get; set; }
        public string Status { get; set; }

    }
}
