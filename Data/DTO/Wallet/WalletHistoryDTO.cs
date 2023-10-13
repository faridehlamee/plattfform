using Data.DTO.BaseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Wallet
{
    public class WalletHistoryDTO : BaseDto<WalletHistoryDTO, Entites.Entities.Wallet.WalletHistory, int>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public WalletDTO Wallet { get; set; }
        public int WalletId { get; set; }
        public string Operation { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public string StatusDesc { get; set; }
    }
}
