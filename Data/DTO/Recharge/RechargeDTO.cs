using Data.DTO.BaseDTO;
using Data.DTO.WareHouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Recharge
{
    public class RechargeDTO : BaseDto<RechargeDTO, Entites.Entities.Recharge, int>
    {
        public RechargeDTO()
        {
            IsCheck = false;
        }
        public int ProductWareHouseId { get; set; }
        public string ProductWareHouseTypeSizeItemName { get; set; }
        //public ProductWareHouseDTO ProductWareHouse { get; set; }
        public string PhonNumber { get; set; }
        public bool IsCheck { get; set; }
    }
}
