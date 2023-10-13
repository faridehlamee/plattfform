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
    [Table("StoreType", Schema = "BPR")]
    public class StoreType : BaseEntity<int>, IEntity<int>
    {
        public string StoreName { get; set; }
        public string Image { get; set; }

        public int GenderProductTypeId { get; set; }
        public virtual GenderProductType GenderProductType { get; set; }

        //public int? TypeSizeId { get; set; }
        //public virtual TypeSize TypeSize { get; set; }


    }
}
