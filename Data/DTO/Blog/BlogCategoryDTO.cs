using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Blog
{
    public class BlogCategoryDTO : BaseDto<BlogCategoryDTO, Entites.Entities.BlogCategory, int>
    {
        public string Name { get; set; }
 
    }
}
