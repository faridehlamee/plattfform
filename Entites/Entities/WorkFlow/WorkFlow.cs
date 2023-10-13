using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.WorkFlow
{
    [Table("WorkFlow", Schema = "WO")]
    public class WorkFlow : BaseEntity<int>, IEntity<int>
    {
        public string EntityType { get; set; }
        public int Index { get; set; }
        public string Status { get; set; }
        public int ApproveId { get; set; }
        public User.User Approve { get; set; }

        // اینجا قابل دولوپ میتونه باشه نسبت به بیزینس 
        //مثلا بر اساس user مشخص شه

    }
}
