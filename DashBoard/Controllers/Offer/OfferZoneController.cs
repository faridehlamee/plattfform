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
    public class OfferZoneController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<OfferZone> _offerZoneRepository;

        public OfferZoneController(IMapper Mapper, IRepository<OfferZone> OfferZoneRepository)
        {
            _mapper = Mapper;
            _offerZoneRepository = OfferZoneRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _offerZoneRepository.TableNoTracking.ProjectTo<OfferZoneDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(OfferZoneDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _offerZoneRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "OfferZone");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _offerZoneRepository.TableNoTracking.ProjectTo<OfferZoneDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(OfferZoneDTO model, CancellationToken cancellationToken)
        {
            var data = await _offerZoneRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _offerZoneRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "OfferZone");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _offerZoneRepository.GetByIdAsync(cancellationToken, Id);
            await _offerZoneRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
