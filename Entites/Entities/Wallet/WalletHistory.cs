using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Wallet
{
    [Table("WalletHistory", Schema = "FIN")]
    public class WalletHistory : BaseEntity<int>, IEntity<int>
    {
        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }
        public string Operation { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
    }
}
