using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Payment
{
    public class VerifyResponseDTO
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public long? RefID { get; set; }
    }
}
