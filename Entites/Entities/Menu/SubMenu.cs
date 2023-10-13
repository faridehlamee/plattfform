using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Menu
{
    [Table("SubMenu", Schema = "CO")]
    public class SubMenu : BaseEntity<int>, IEntity<int>
    {
        public int Level { get; set; }
        public string SubTitle { get; set; }

        public string URL { get; set; }


        public int MenuId { get; set; }
        public Menu Menu { get; set; }

        public int? ParentId { get; set; }
        public SubMenu Parent { get; set; }
    }
}
