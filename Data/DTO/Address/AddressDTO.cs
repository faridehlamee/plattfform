using Data.DTO.BaseDTO;
using Data.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Address
{
    public class AddressDTO : BaseDto<AddressDTO, Entites.Entities.Address, int>
    {
        public int UserId { get; set; }
        //public virtual UserDTO User { get; set; }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        //public virtual ProvinceDTO Province { get; set; }


        public int CityId { get; set; }
        public string CityName { get; set; }

        //public virtual CityDTO City { get; set; }

        public string Receiver { get; set; }
        public string AddressDesc { get; set; }
        public string Plaque { get; set; }
        public string PostalCode { get; set; }
    }
}
