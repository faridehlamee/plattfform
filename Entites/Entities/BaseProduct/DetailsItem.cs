using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("DetailsItem", Schema = "BPR")]
    public class DetailsItem : BaseEntity<int>, IEntity<int>
    {
        public string SubTopic { get; set; }
        public int DetailsId { get; set; }
        public virtual Details Details { get; set; }

    }
}
