using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseDTO
{
    public static class Pagination
    {
        public static Pagedata<T> PageResult<T>(this List<T> list, int PageNumber, int PageSize) where T : class
        {
            var result = new Pagedata<T>();
            result.Resualt = list.Skip(PageSize * (PageNumber - 1)).Take(PageSize).ToList();
            result.TotalPages = Convert.ToInt32(Math.Ceiling((double)list.Count() / PageSize));
            result.CurrentPage = PageNumber;
            return result;
        }


    }

}
