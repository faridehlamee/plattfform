using SmsIrRestfulNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class SmsDTO
    {
        public SmsDTO()
        {
            parameters = new List<Parmeter>();
        }
        public int templateId { get; set; }
        public string mobile { get; set; }
        public List<Parmeter> parameters { get; set; }
    }

    public class Parmeter
    {

        public string name { get; set; }
        public string value { get; set; }
    }

    public class ResultSms
    {
        public int status { get; set; }
        public string message { get; set; }
        public ResultSmsData data { get; set; }
    }
    public class ResultSmsData
    {
        public int messageId { get; set; }
        public float cost { get; set; }
    }
}
