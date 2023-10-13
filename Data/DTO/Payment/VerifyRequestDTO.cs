using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Payment
{
    public class VerifyRequestDTO
    {
        public int Amount { get; set; }
        public string Authority { get; set; }
        public string MerchantID { get; set; }
    }
}
