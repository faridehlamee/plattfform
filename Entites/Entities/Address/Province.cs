using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Province", Schema = "LO")]
    public class Province : BaseEntity , IEntity<int>
    {
        public string Name { get; set; }

    }
}
