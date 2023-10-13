using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Payment
{
    public class PaymentResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Authority { get; set; }
        public string PaymentUrl { get; set; }
    }
}
