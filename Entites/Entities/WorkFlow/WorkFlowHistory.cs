using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.WorkFlow
{
    [Table("WorkFlowHistory", Schema = "WO")]
    public class WorkFlowHistory : BaseEntity<int>, IEntity<int>
    {
        public int EntityId { get; set; }
        public string EntityType { get; set; }
        public string Status { get; set; }
        public int Index { get; set; }
        public int ApproveId { get; set; }
        public User.User Approve { get; set; }
        public bool IsConfirm { get; set; }
        public string Comment { get; set; }
        public string File { get; set; }
    }
}
