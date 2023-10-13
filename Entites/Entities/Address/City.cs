using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("City", Schema = "LO")]
    public class City : BaseEntity , IEntity<int>
    {
        public string Name { get; set; }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }


    }
}
