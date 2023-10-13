using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace Entites.Entities
{
    [Table("Details", Schema = "BPR")]
    public class Details : BaseEntity<int>, IEntity<int>
    {
        public string Title { get; set; }
        public ICollection<DetailsItem> ListDetailsItem { get; set; }
    }
}
