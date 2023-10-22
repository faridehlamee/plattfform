using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("InfoSite", Schema = "CO")]
    public class InfoSite : BaseEntity<int>, IEntity<int>
    {
        public InfoSite()
        {
            IsCheck = false;
        }

        public int Key { get; set; }
        public string Value { get; set; }
       
        public bool IsCheck { get; set; }
    }
}
