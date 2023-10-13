using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities
{
    [Table("Slider", Schema = "CO")]
    public class Slider : BaseEntity<int>, IEntity<int>
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

    }
}
