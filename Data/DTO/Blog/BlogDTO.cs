using Data.DTO.BaseDTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Blog
{
    public class BlogDTO : BaseDto<BlogDTO, Entites.Entities.Blog, int>
    {
        public int[] BlogCategoryIds { get; set; }
        public int BlogCategoryId { get; set; }
        public string BlogCategoryName { get; set; }

        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Sumary { get; set; }
        public string AparatID { get; set; }
        public string AparatLink { get; set; }

        public List<SelectListItem> ListBlogCategory { get; set; }



    }
}
