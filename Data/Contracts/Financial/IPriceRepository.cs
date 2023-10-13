using Data.Repositories;
using Entites.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace  Data.Contracts.Financial
{
    public interface IPriceRepository : IRepository<Price_Log>
    {
        Task<Price_Log> GetPrice(int productId);

        Task AddNewPrice(int productId, double amount, CancellationToken cancellationToken);

    }
}