using Data.DTO.BaseDTO;
using Data.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.WorkFlow
{
    public class WorkFlowHistoryDTO : BaseDto<WorkFlowHistoryDTO, Entites.Entities.WorkFlow.WorkFlowHistory, int>
    {
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public int Index { get; set; }
        public int ApproveId { get; set; }
        public UserDTO Approve { get; set; }
        public bool IsConfirm { get; set; }
        public string Comment { get; set; }
    }
}
