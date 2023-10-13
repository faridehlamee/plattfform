using Entities.Common;
using Entities.Entities.WareHouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("GenderProductType", Schema = "BPR")]
    public class GenderProductType : BaseEntity<int>, IEntity<int>
    {
        public string Name { get; set; }
        public string Image { get; set; }

    }
}
