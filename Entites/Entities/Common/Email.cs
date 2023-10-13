using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    [Table("Email", Schema = "CO")]
    public class Email : BaseEntity<int>, IEntity<int>
    {
        public Email()
        {
            IsCheck = false;
        }

        public string Fullname { get; set; }
        public string EmailAddress { get; set; }
        public string Mobile { get; set; }
        public string Subject { get; set; }
        public string message { get; set; }
        public bool IsCheck { get; set; }
    }
}
