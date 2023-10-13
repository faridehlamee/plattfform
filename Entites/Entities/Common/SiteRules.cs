using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("SiteRules", Schema = "CO")]
    public class SiteRules : BaseEntity<int>, IEntity<int>
    {
        public string Description { get; set; }

    }
}
