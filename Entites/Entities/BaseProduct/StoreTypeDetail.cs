using Entities.Common;
using Entities.Entities.WareHouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("StoreTypeDetail", Schema = "BPR")]
    public class StoreTypeDetail : BaseEntity<int>, IEntity<int>
    {
        public int StoreTypeId { get; set; }
        public virtual StoreType StoreType { get; set; }

        public int DetailsId { get; set; }
        public virtual Details Details { get; set; }

    }
}
