using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("BlogCategory", Schema = "BL")]
    public class BlogCategory : BaseEntity<int>, IEntity<int>
    {
        public string Name { get; set; }
    }
}
