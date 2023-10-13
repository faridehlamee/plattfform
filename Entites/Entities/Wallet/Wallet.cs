using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Wallet
{
    [Table("Wallet", Schema = "FIN")]
    public class Wallet : BaseEntity<int>, IEntity<int>
    {
        public int UserId { get; set; }
        public User.User User { get; set; }
        public double Balance { get; set; }
    }
}
