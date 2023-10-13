using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseDTO
{

    public class ResultDTO
    {

        public bool Status { get; set; }
        public string Messages { get; set; }
    }
    public class ResultDTO<T>:ResultDTO
    {
        public T Data { get; set; }
        public List<T> ListData { get; set; }

    }
}
