using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Guide", Schema = "PRO")]
    public class Guide : BaseEntity<int>, IEntity<int>
    {
        public string Description { get; set; }

        public string ImageFile { get; set; }

        public int? StoreTypeId { get; set; }
        public virtual StoreType StoreType { get; set; }
    }
}
