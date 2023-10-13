using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.Post
{
    [Table("PostCategory", Schema = "BL")]
    public class PostCategory : BaseEntity<int>, IEntity<int>
    {
        [Required]
        public string Name { get; set; }
        public int? ParentPostCategoryId { get; set; }

        [ForeignKey(nameof(ParentPostCategoryId))]
        public PostCategory ParentPostCategory { get; set; }
        public ICollection<PostCategory> ChildCategories { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
