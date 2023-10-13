using Data.DTO.BaseProduct;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseDTO
{
    public class FilterDTO
    {
        public string Title { get; set; }
        public List<SelectListItem> Item { get; set; }
    }
}
