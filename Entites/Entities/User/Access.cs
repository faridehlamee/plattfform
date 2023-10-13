using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Access", Schema = "ACC")]
    public class Access : BaseEntity , IEntity<int>
    {
        public string Title { get; set; }

    }
}
