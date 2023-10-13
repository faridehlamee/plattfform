using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.User
{
    [Table("IdentificationCode_log", Schema = "ACC")]
    public class IdentificationCode_log : BaseEntity, IEntity<int>
    {
        public int UserId { get; set; }
        public int IdentificationCodeRuleId { get; set; }
    }
}
