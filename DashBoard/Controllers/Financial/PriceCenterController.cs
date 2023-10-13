using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.Financial;
using Data.Repositories;
using Entites.Entities.Financial;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Financial
{
    [Authorize(Roles = "Admin")]
    public class PriceCenterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<PriceCenter> _priceCenterRepository;
        private readonly IRepository<PriceCenterItem> _priceCenterItemRepository;

        public PriceCenterController(IMapper Mapper, IRepository<PriceCenter> PriceCenterRepository , IRepository<PriceCenterItem> PriceCenterItemRepository)
        {
            _mapper = Mapper;
            _priceCenterRepository = PriceCenterRepository;
            _priceCenterItemRepository = PriceCenterItemRepository;
        }

        #region PriceCenter
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _priceCenterRepository.TableNoTracking.ProjectTo<PriceCenterDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PriceCenterDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _priceCenterRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "PriceCenter");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _priceCenterRepository.TableNoTracking.ProjectTo<PriceCenterDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(PriceCenterDTO model, CancellationToken cancellationToken)
        {
            var data = await _priceCenterRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _priceCenterRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "PriceCenter");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _priceCenterRepository.GetByIdAsync(cancellationToken, Id);
            await _priceCenterRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
        #endregion


        #region PriceCenterItem

        public IActionResult PriceCenterItemCreate(int PriceCenterId) { var data = new PriceCenterItemDTO() { PriceCenterId= PriceCenterId}; return View(); }

        public async Task<JsonResult> PriceCenterItemListAsync(CancellationToken cancellationToken , int PriceCenterId)
        {
            var dto = await _priceCenterItemRepository.TableNoTracking.ProjectTo<PriceCenterItemDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.PriceCenterId == PriceCenterId)
                .ToListAsync();

            return Json(dto);
        }
        #endregion
    }
}
