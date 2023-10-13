using Entities.Common;
using Entities.Entities.WareHouse;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Entites.Entities.Product
{
    [Table("Product", Schema = "PRO")]
    public class Product : BaseEntity<int>, IEntity<int>
    {
        public string ProductCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tags { get; set; }
        public int Group { get; set; }

        public double Amount { get; set; }
        public string CurrentImage { get; set; }
        public int? SizeGuideId { get; set; }
        public virtual Guide Guide { get; set; }

        public int ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; }

        public int StoreTypeId { get; set; }
        public virtual StoreType StoreType { get; set; }

        public int? TypeSizeId { get; set; }
        public virtual TypeSize TypeSize { get; set; }

        public bool IsShow { get; set; }
        public ICollection<Discount.Discount> Discount { get; set; }
        public ICollection<ProductDetail> details { get; set; }
        public ICollection<WareHouse.ProductWareHouse> ProductWareHouses { get; set; }

    }
}