using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities
{
    [Table("Team", Schema = "CO")]
    public class Team : BaseEntity<int>, IEntity<int>
    {
        public string Image { get; set; }
        public string FullName { get; set; }
        public string JobDescription { get; set; }
        public string Bio { get; set; }
        public string LinkedIdUrl { get; set; } 

    }
}
