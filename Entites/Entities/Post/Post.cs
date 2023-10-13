using Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities.Post
{
    [Table("Post", Schema = "BL")]
    public class Post : BaseEntity<int>, IEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }


        public int PostCategoryId { get; set; }

        [ForeignKey(nameof(PostCategoryId))]
        public PostCategory PostCategory { get; set; }

    }
    //FeluentApi برای کانفیگ ستون ها
    public class PosConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Description).IsRequired();
            builder.HasOne(p => p.PostCategory).WithMany(c => c.Posts).HasForeignKey(p => p.PostCategoryId);
        }
    }
}
