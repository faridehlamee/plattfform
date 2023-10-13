using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("ViewPage", Schema = "CO")]
    public class ViewPage : BaseEntity<int>, IEntity<int>
    {
        public string SystemIp { get; set; }
        public int? ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

        public int? BlogId { get; set; }
        public virtual Blog Blog { get; set; }

        public DateTime Date { get; set; }

    }
}
