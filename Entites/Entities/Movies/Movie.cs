using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Movies
{
    [Table("Movie", Schema = "MO")]
    public class Movie : BaseEntity<int>, IEntity<int>
    {
        public int MovieCategoryId { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AparatID { get; set; }
        public string AparatLink { get; set; }
        public int NumberShow { get; set; }
        public int Rates { get; set; }
    }
}
