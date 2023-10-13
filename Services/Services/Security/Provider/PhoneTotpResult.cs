using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Security.Provider
{
    public class PhoneTotpResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }
    }
}
