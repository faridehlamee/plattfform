using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities
{
    [Table("Portfolio", Schema = "CO")]
    public class Portfolio : BaseEntity<int>, IEntity<int>
    {
        public string Image { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string CompletionTime { get; set; }
        public DateTime ProjectDate { get; set; } 
        public string Url { get; set; }
        public string Detail { get; set; }

    }
}
