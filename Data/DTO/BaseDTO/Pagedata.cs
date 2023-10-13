using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseDTO
{
    public class Pagedata<T> where T : class
    {
        public IEnumerable<T> Resualt { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        //public List<FilterDTO> Filter { get; set; }
        public int TotalProduct { get; set; }
    } 
}
