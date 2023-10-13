using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Data.Contracts.Wallet;
using Data.DTO.Product;
using Data.DTO.Wallet;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers.Wallet
{
    [Authorize(Roles = "Admin")]
    public class WalletController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IWalletHistoryRepository _walletHistoryRepository;
        private readonly IWalletRepository _walletRepository;

        public WalletController(IMapper mapper ,IWalletHistoryRepository walletHistoryRepository , IWalletRepository walletRepository)
        {
            _mapper = mapper;
            _walletHistoryRepository = walletHistoryRepository;
            _walletRepository = walletRepository;
        }
        public  async Task<IActionResult> Index(SearchDTO model, WalletDTO Search)
        {
            
            return View();
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, WalletDTO Search, CancellationToken cancellationToken)
        {
            var dto = await _walletRepository.GetListWallet(model, Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }
        public async Task<IActionResult> UserWallet(int Id)
        {
            var data = await _walletRepository.TableNoTracking.
                Select(c=> new WalletDTO { 
                Id=c.Id,
                UserId = c.UserId,
                UserFullName = $"{c.User.FirstName} {c.User.LastName}"
                })
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> ChargeWallet(Data.DTO.Wallet.WalletDTO data, CancellationToken cancellationToken)
        {
            string Message = "شارژ دستی انجام شد";
            var res = await _walletRepository.ChargeWallet(data.UserId, data.Balance, Message, cancellationToken);
            return Redirect("Index");
        }
        public IActionResult Transaction()
        { 
            var data = new WalletHistoryDTO();
            return View(data);
        }
        public async Task<JsonResult> TransactionListAsync(SearchDTO model, Data.DTO.Wallet.WalletHistoryDTO Search, CancellationToken cancellationToken)
        {
            var dto = await _walletHistoryRepository.GetTransaction(model, Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }
    }
}
