using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseDTO
{
    public class BasePaging
    {
        public BasePaging()
        {
            page = 1;
            take = 32;
        }


        public int page { get; set; }

        public int pageSize { get; set; }

        //public int ActivePage { get; set; }

        //public int StartPage { get; set; }

        //public int EndPage { get; set; }

        public int take { get; set; }

        //public int SkipEntity { get; set; }
    }
}

