using Entities.Common;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.Enums.Product;

namespace Entities.Entities.WareHouse
{
    [Table("TypeSize", Schema = "PW")]
    public class TypeSize : BaseEntity, IEntity<int>
    {
        public string Name { get; set; }
        public TypeWarehouse  TypeWarehouse  { get; set; }

    }
}
