using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("FAQ", Schema = "CO")]
    public class FAQ : BaseEntity<int>, IEntity<int>
    {
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
