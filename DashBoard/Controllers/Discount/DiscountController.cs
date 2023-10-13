using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.Discount;
using Data.Contracts.Offer;
using Data.Contracts.OfferItem;
using Data.Contracts.User;
using Data.DTO.Discount;
using Data.DTO.Offer;
using Data.DTO.Product;
using Data.DTO.User;
using Entites.Entities.Discount;
using Entites.Entities.Offer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers.Discount
{
    [Authorize(Roles = "Admin")]
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IDiscountCodeUsedRepository _discountCodeUsedRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public DiscountController(IDiscountRepository discountRepository, IDiscountCodeUsedRepository discountCodeUsedRepository, IMapper Mapper, IUserRepository UserRepository)
        {
            _mapper = Mapper;
            _userRepository = UserRepository;
            _discountRepository = discountRepository;
            _discountCodeUsedRepository = discountCodeUsedRepository;
        }
        public IActionResult Index(int discountType)
        {
            var data = new DiscountDTO() { DiscountType = (Common.AllEnum.Commons.DiscountType)discountType };

            return View(data);
        }
        public async Task<JsonResult> ListAsync(SearchDTO model, DiscountDTO Search, CancellationToken cancellationToken)
        {
            Search.DiscountType = Common.AllEnum.Commons.DiscountType.discount;
            var dto = await _discountRepository.GetList(model, Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public IActionResult Create(int discountType)
        {
            var data = new DiscountDTO() { DiscountType = (Common.AllEnum.Commons.DiscountType)discountType };
            return View(data);

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(DiscountDTO model)
        {
            var data = model.ToEntity(_mapper);
            var checkDisCountKey = await _discountRepository.TableNoTracking.Where(c => c.KeyDiscountPercent.Equals(model.KeyDiscountPercent)).AnyAsync();
            if (!checkDisCountKey)
            {
                await _discountRepository.AddAsync(data, CancellationToken.None);
                return RedirectToAction("Index", "Discount");
            }
            else
            {
                return RedirectToAction("Index", "Discount");
            }

        }
        public async Task<IActionResult> Editpage(int id)
        {
            var data = await _discountRepository.TableNoTracking.ProjectTo<DiscountDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(DiscountDTO model, CancellationToken cancellationToken)
        {
            var data = await _discountRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            return RedirectToAction("Index", "Discount");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _discountRepository.GetByIdAsync(cancellationToken, Id);
            await _discountRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }



        public IActionResult AddUser(int DiscountCodeId) { var user = new UserDTO() { DiscountId = DiscountCodeId }; return View(user); }
        public async Task<JsonResult> AddUserListAsync(SearchDTO model, UserDTO Search, CancellationToken cancellationToken)
        {
            var dto = await _userRepository.GetListUser(model, Search);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }
        public async Task<JsonResult> FuncAddUserAsync(int[] UserIds, int DiscountId, CancellationToken cancellationToken)
        {
            try
            {
                var listdicountCode = new List<DiscountCodeUsed>();
                var PermittedNumber = await _discountRepository.GetNumberAllow(DiscountId);
                if (UserIds.Count() > 0)
                {
                    foreach (var item in UserIds)
                    {
                        for (int i = 0; i < PermittedNumber; i++)
                        {
                            var dicountCode = new DiscountCodeUsed()
                            {
                                UserId = item,
                                DiscountId = DiscountId,
                                IsCompleted = false

                            };

                            listdicountCode.Add(dicountCode);
                        }

                    }

                    await _discountCodeUsedRepository.AddRangeAsync(listdicountCode, cancellationToken, true);
                    return Json(true);

                }
                return Json(false);
            }
            catch (Exception)
            {
                return Json(false);
            }


        }


        public IActionResult RemoveUser(int DiscountCodeId) { var data = new DiscountCodeUsedDTO() { DiscountId = DiscountCodeId }; return View(data); }

        public async Task<JsonResult> RemoveUserListAsync(SearchDTO param, DiscountCodeUsedDTO model, CancellationToken cancellationToken)
        {
            var dto = await _discountCodeUsedRepository.GetList(param, model);
            return Json(new { total = dto.TotalPages, data = dto.Resualt });
        }

        public async Task<bool> RemoveUserDiscount(int id, CancellationToken cancellationToken)
        {
            var data = await _discountCodeUsedRepository.GetByIdAsync(cancellationToken, id);
            await _discountCodeUsedRepository.DeleteIsActiveAsync(data, cancellationToken);
            return true;
        }


    }
}
