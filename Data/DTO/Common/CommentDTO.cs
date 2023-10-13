using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.User;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class CommentDTO : BaseDto<CommentDTO, Comment, int>
    {
          public CommentDTO()
        {
            IsShow = false;
        }

        public int? ProductId { get; set; }
        //public virtual ProductDTO Product { get; set; }

        public int? UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        //public virtual UserDTO User { get; set; }

        public string CommentText { get; set; }

        public int? ParentId { get; set; }
        public virtual CommentDTO Parent { get; set; }

        public bool IsShow { get; set; }
    }
}
