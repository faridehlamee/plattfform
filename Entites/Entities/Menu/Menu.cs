using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Menu
{
    [Table("Menu", Schema = "CO")]
    public class Menu : BaseEntity<int>, IEntity<int>
    {
        public string Image { get; set; }
        public string Title { get; set; }
        public string Icon { get; set; }
        public int Level { get; set; }
        public string URL { get; set; }


        public int? ParentId { get; set; }
        public Menu Parent { get; set; }

        public ICollection<SubMenu> CltSubMenu { get; set; }
    }
}
