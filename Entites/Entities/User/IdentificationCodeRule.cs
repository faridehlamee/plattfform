using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities.User
{
    [Table("IdentificationCodeRule", Schema = "ACC")]
    public class IdentificationCodeRule : BaseEntity, IEntity<int>
    {
        public bool ForOwner { get; set; }
        public bool ForUser { get; set; }
        public bool ForHasDisCount { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public TypeOffPrice? TypeOffPrice { get; set; }
        public double? Value { get; set; }

    }
}
 