using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Post
{
    public class PostDTO : BaseDto<PostDTO, Entities.Entities.Post.Post, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }


        public int PostCategoryId { get; set; }
        public PostCategoryDTO PostCategory { get; set; }
    }
}
