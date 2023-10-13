using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Comment", Schema = "CO")]
    public class Comment : BaseEntity<int>, IEntity<int>
    {

        public Comment()
        {
            IsShow = false;
        }

        public int? ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

        public int? UserId { get; set; }
        public virtual Entites.Entities.User.User User { get; set; }

        public string CommentText { get; set; }

        public int? ParentId { get; set; }
        public virtual Comment Parent { get; set; }

        public bool IsShow { get; set; }


    }
}
