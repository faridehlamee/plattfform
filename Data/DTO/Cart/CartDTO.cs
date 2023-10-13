using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Cart
{
    public class CartDTO : BaseDto<CartDTO, Entites.Entities.Cart.Cart, int>
    {
        
        public string key { get; set; }

        public int ProductId { get; set; }
        public double ProductAmount { get; set; }
        public string ProductName { get; set; }
        public string ProductCurrentImage { get; set; }
        //public virtual ProductDTO Product { get; set; }
        public double FinalAmount { get; set; }
        public int ProductWareHouseId { get; set; }
        public string ProductWareHouseTypeSizeItemName { get; set; }
        //public virtual ProductWareHouseDTO ProductWareHouse { get; set; }

        public double Value { get; set; }

        public double TotalAmount { get {
                try
                {
                    return Value * FinalAmount;
                }
                catch (Exception)
                {
                    return 0;
                }
            
            } }

    }


    public class InsertCartDTO : BaseDto<InsertCartDTO, Entites.Entities.Cart.Cart, int>
    {
        public string key { get; set; }
        public int ProductId { get; set; }
        public double FinalAmount { get; set; }
        public int ProductWareHouseId { get; set; }
        public double Value { get; set; }
    }
}
