using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities
{
    [Table("Pricing", Schema = "CO")]
    public class Pricing : BaseEntity<int>, IEntity<int>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public string Options { get; set; } 

    }
}
