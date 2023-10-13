using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Blog", Schema = "BL")]
    public class Blog : BaseEntity<int>, IEntity<int>
    {
        public int BlogCategoryId { get; set; }
        public BlogCategory BlogCategory { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sumary { get; set; }
        public string AparatID { get; set; }
        public string AparatLink { get; set; }
    }
}
