using Data.DTO.BaseDTO;
using Data.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Wallet
{
    public class WalletDTO : BaseDto<WalletDTO, Entites.Entities.Wallet.Wallet, int>
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserPhone { get; set; }
        public double Balance { get; set; }
    }
}
