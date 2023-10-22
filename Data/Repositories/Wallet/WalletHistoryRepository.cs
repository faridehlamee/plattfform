using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts.Wallet;
using Data.DTO.BaseDTO;
using Data.DTO.Product;
using Data.DTO.Wallet;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Wallet
{
    public class WalletHistoryRepository : Repository<Entites.Entities.Wallet.WalletHistory>, IWalletHistoryRepository, IScopedDependency
    {
        private readonly IMapper _mapper;

        public WalletHistoryRepository(IMapper Mapper, RoyalCanyonDBContext dbContext, IHttpContextAccessor contextAccessor)
            : base(dbContext, contextAccessor)
        {
            _mapper = Mapper;
        }

        public async Task<Pagedata<WalletHistoryDTO>> GetTransaction(SearchDTO model, WalletHistoryDTO Search)
        {
            var data = new Pagedata<WalletHistoryDTO>();

            var query = TableNoTracking.Where(c => c.IsActive);

            //query = await Filter(model, query, Search);
            if (Search.FirstName!=null)
            {
                query = query.Where(c => c.Wallet.User.FirstName.Contains(Search.FirstName));
            }
            if (Search.LastName != null)
            {
                query = query.Where(c => c.Wallet.User.LastName.Contains(Search.LastName));
            }
            if (Search.UserName != null)
            {
                query = query.Where(c => c.Wallet.User.UserName.Contains(Search.UserName));
            }
            query = query.Include(y => y.Wallet).Include(c => c.Wallet.User);
            data.Resualt = await query.Select(w => new WalletHistoryDTO
            {
                Id = w.Id,
                DateInsert = w.DateInsert,
                WalletId = w.WalletId,
                UserName = w.Wallet.User.UserName,
                FirstName=w.Wallet.User.FirstName ,
                LastName= w.Wallet.User.LastName,
                Operation = w.Operation,
                Amount = w.Amount,
                Status = w.Status,
                StatusDesc = w.StatusDesc

            })
                  .OrderByDescending(d=> d.DateInsert).Skip(model.take * (model.page - 1))
                 .Take(model.take).ToListAsync();


            //این دو خط پایین برای react
            //  var ProductIds = await query.Select(c => c.Id).ToArrayAsync();
            //data.Filter = await MakeFilter(ProductIds);
            data.CurrentPage = model.page;
            double total = await query.CountAsync();
            data.TotalPages = (int)Math.Ceiling(total); /// model.take
            return data;
        }


    }
}
