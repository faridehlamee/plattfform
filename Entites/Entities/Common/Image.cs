using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Image", Schema = "CO")]
    public class Image : BaseEntity<int>, IEntity<int>
    {
        public string ImageFile { get; set; }
        public int? EntityId { get; set; }
        public string EntityType { get; set; }
        public int Priority { get; set; }
    }
}
