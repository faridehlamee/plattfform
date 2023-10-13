using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Payment
{
    public class PaymentRequestDTO
    {
        public string MerchantID { get; set; }
        public int Amount { get; set; }
        public string InvoiceID { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string CallbackURL { get; set; }
    }
}
