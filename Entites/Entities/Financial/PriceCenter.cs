using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Financial
{

    [Table("PriceCenter", Schema = "FIN")]
    public class PriceCenter : BaseEntity<int>, IEntity<int>
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
