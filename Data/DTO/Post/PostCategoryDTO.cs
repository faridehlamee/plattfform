using Data.DTO.BaseDTO;
using Entities.Entities.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Post
{
    public class PostCategoryDTO : BaseDto<PostCategoryDTO, PostCategory, int>
    {
        public string Name { get; set; }
        public int? ParentPostCategoryId { get; set; }
        public PostCategory ParentPostCategory { get; set; }
        public ICollection<PostCategoryDTO> ChildCategories { get; set; }
        public ICollection<PostDTO> Posts { get; set; }
    }
}
