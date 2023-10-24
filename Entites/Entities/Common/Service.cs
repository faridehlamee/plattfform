using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities
{
    [Table("Service", Schema = "CO")]
    public class Service : BaseEntity<int>, IEntity<int>
    {
        public string Icone { get; set; }
        public string Name { get; set; }
        public string Decription { get; set; }

        public string Image { get; set; }

    }
}
