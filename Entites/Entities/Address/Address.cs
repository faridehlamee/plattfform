using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities
{
    [Table("Address", Schema = "LO")]
    public class Address : BaseEntity<int> , IEntity<int>
    {
        public int UserId { get; set; }
        public virtual Entites.Entities.User.User User { get; set; }

        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }


        public int CityId { get; set; }
        public virtual City City { get; set; }

        public string Receiver { get; set; }
        public string AddressDesc { get; set; }
        public string Plaque { get; set; }
        public string PostalCode { get; set; }



    }
}
