using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.WareHouse
{
    [Table("TypeSizeItem", Schema = "PW")]
    public class TypeSizeItem : BaseEntity, IEntity<int>
    {
        public string Name { get; set; }
        public int TypeSizeId { get; set; }
        public virtual TypeSize TypeSize { get; set; }
    }
}
