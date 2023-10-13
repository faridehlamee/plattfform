using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.Offer;
using Data.Contracts;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DashBoard.Controllers.Offer
{
    [Authorize(Roles = "Admin")]
    public class OfferTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<OfferType> _offerTypeRepository;

        public OfferTypeController(IMapper Mapper, IRepository<OfferType> OfferTypeRepository)
        {
            _mapper = Mapper;
            _offerTypeRepository = OfferTypeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _offerTypeRepository.TableNoTracking.ProjectTo<OfferTypeDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(OfferTypeDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _offerTypeRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "OfferType");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _offerTypeRepository.TableNoTracking.ProjectTo<OfferTypeDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(OfferTypeDTO model, CancellationToken cancellationToken)
        {
            var data = await _offerTypeRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _offerTypeRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "OfferType");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _offerTypeRepository.GetByIdAsync(cancellationToken, Id);
            await _offerTypeRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
