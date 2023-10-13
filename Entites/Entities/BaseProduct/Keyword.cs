using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.BaseProduct
{
    [Table("Keyword", Schema = "BPR")]
    public class Keyword : BaseEntity<int>, IEntity<int>
    {
        public string Key { get; set; }
    }
}
