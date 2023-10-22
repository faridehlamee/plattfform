using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Data.Contracts.Wallet;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Wallet;
using Entites.Entities;
using Entites.Entities.Wallet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Wallet
{
    public class WalletRepository : Repository<Entites.Entities.Wallet.Wallet>, IWalletRepository, IScopedDependency
    {
        private readonly IWalletHistoryRepository _walletHistoryRepository;

        public WalletRepository(RoyalCanyonDBContext dbContext , IWalletHistoryRepository WalletHistoryRepository, IHttpContextAccessor contextAccessor)
            : base(dbContext  , contextAccessor)
        {
            _walletHistoryRepository = WalletHistoryRepository;
        }

        public async Task<Entites.Entities.Wallet.Wallet> GetbyUserId(int UserId)
        {
            var data = await TableNoTracking.Where(c => c.UserId == UserId).SingleOrDefaultAsync();
            return data;
        }

        public async Task<Entites.Entities.Wallet.Wallet> ChargeWallet(int UserId , double Amount ,string Message, CancellationToken cancellationToken)
        {
            var data = await Table.Where(c => c.UserId == UserId).SingleOrDefaultAsync();
            data.Balance = data.Balance + Amount;

            var History = new WalletHistory()
            {
                WalletId = data.Id,
                Amount = Amount,
                Balance = data.Balance,
                Status=200,
                StatusDesc= Message,
                Operation ="+"
                
            };

            await _walletHistoryRepository.AddAsync(History, cancellationToken);
            await UpdateAsync(data, cancellationToken);

            return data;
        }

        public async Task<bool> Pay( double finalpayment, int userId, CancellationToken cancellationToken)
        {
            try
            {
                var walllet = await GetbyUserId( userId);
                if (walllet.Balance < finalpayment)
                    return false;
                walllet.Balance = walllet.Balance - finalpayment;
                await UpdateAsync(walllet, cancellationToken);
                var log = new WalletHistory()
                {
                    Amount = finalpayment,
                    Status=200,
                    StatusDesc="پرداخت از کیف پول",
                    Balance = walllet.Balance,
                    WalletId = walllet.Id,
                    Operation = "-"

                };
                await _walletHistoryRepository.AddAsync(log, cancellationToken);

                return true;
            }
            catch (System.Exception)
            {

                return false;
            }
           
            
        }

        public async Task<Pagedata<WalletDTO>> GetListWallet(SearchDTO model, WalletDTO search)
        {
            var data = new Pagedata<WalletDTO>();

            var query = TableNoTracking.Where(c => c.IsActive);

            if (search.UserFullName != null && search.UserFullName!="")
            {
                query = query.Where(c => c.User.FirstName.Contains(search.UserFullName) || c.User.LastName.Contains(search.UserFullName));

            }
            if (search.UserPhone != null && search.UserPhone != "")
            {
                query = query.Where(c => c.User.PhoneNumber.Contains(search.UserPhone));

            }

            query = query.Include(c => c.User);
            data.Resualt = await query.Select(w => new WalletDTO
            {
                Id =w.Id,
                Balance=w.Balance,
                UserFullName = $"{w.User.FirstName} {w.User.LastName}",
                UserPhone = w.User.PhoneNumber
                
            }).Skip(model.take * (model.page - 1))
               .Take(model.take).ToListAsync();

            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total); /// model.take
            return data;
        }
    }
}






