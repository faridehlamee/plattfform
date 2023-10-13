using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Wallet;
using Data.Repositories;
using Entites.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Wallet
{
    public interface IWalletHistoryRepository : IRepository<Entites.Entities.Wallet.WalletHistory>
    {
        Task<Pagedata<WalletHistoryDTO>> GetTransaction(SearchDTO model, WalletHistoryDTO Search);
    }
}