using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Movies
{
    [Table("MovieCategory", Schema = "MO")]
    public class MovieCategory : BaseEntity<int>, IEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
