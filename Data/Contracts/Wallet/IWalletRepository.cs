using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Wallet;
using Data.Repositories;
using Entites.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Wallet
{
    public interface IWalletRepository : IRepository<Entites.Entities.Wallet.Wallet>
    {
        Task<Entites.Entities.Wallet.Wallet> GetbyUserId(int UserId);
        Task<Entites.Entities.Wallet.Wallet> ChargeWallet(int UserId, double Amount, string Message, CancellationToken cancellationToken);

        Task<bool> Pay(double finalpayment, int userId, CancellationToken cancellationToken);
        Task<Pagedata<WalletDTO>> GetListWallet(SearchDTO model, WalletDTO search);
    }
}