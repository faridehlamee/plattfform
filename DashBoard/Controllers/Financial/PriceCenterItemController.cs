using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.Financial;
using Data.Repositories;
using Entites.Entities.Financial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Financial
{
    [Authorize(Roles = "Admin")]
    public class PriceCenterItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PriceCenter> _priceCenterRepository;
        private readonly IRepository<PriceCenterItem> _priceCenterItemRepository;

        public PriceCenterItemController(IMapper Mapper, IRepository<PriceCenter> PriceCenterRepository , IRepository<PriceCenterItem> PriceCenterItemRepository)
        {
            _mapper = Mapper;
            _priceCenterRepository = PriceCenterRepository;
            _priceCenterItemRepository = PriceCenterItemRepository;
        }

        #region PriceCenterItem
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken, int PriceCenterId)
        {
            var dto = await _priceCenterItemRepository.TableNoTracking.ProjectTo<PriceCenterItemDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.PriceCenterId == PriceCenterId)
                .ToListAsync();

            return Json(dto);
        }


        public IActionResult Create(int PriceCenterId) { var data = new PriceCenterItemDTO() { PriceCenterId= PriceCenterId}; return View(); }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(PriceCenterItemDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _priceCenterItemRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "PriceCenter");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _priceCenterItemRepository.TableNoTracking.ProjectTo<PriceCenterItemDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(PriceCenterItemDTO model, CancellationToken cancellationToken)
        {
            var data = await _priceCenterItemRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _priceCenterItemRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "PriceCenter");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _priceCenterItemRepository.GetByIdAsync(cancellationToken, Id);
            await _priceCenterItemRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
        #endregion
    }
}
