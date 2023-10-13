using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common.Utilities;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.Offer;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Offer
{
    [Authorize(Roles = "Admin")]
    public class OfferController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<Entites.Entities.Offer.Offer> _offerRepository;
        private readonly IRepository<OfferType> _offerTypeRepository;
        private readonly IRepository<OfferZone> _offerZoneRepository;

        public OfferController(IMapper Mapper, IImageRepository ImageRepository, IRepository<Entites.Entities.Offer.Offer> OfferRepository  , IRepository<OfferType> OfferTypeRepository , IRepository<OfferZone> OfferZoneRepository)
        {
            _mapper = Mapper;
            _imageRepository = ImageRepository;
            _offerRepository = OfferRepository;
            _offerTypeRepository = OfferTypeRepository;
            _offerZoneRepository = OfferZoneRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _offerRepository.TableNoTracking.ProjectTo<OfferDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create() 
        {
            var data = new OfferDTO();
            data.ListOfferType = await _offerTypeRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Type
                })
                .ToListAsync();
            data.ListOfferZone = await _offerZoneRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(OfferDTO model)
        {
            model.ExpirDate = model.PersianExpirDate.GetGregorianDate();
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/offer/", form);
                data.Image = imagename;
            }
            await _offerRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Offer");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _offerRepository.TableNoTracking.ProjectTo<OfferDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            data.ListOfferType = await _offerTypeRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Type
                })
                .ToListAsync();
            data.ListOfferZone = await _offerZoneRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(OfferDTO model, CancellationToken cancellationToken)
        {
            var data = await _offerRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/offer/");
                }
                var imagename = _imageRepository.SaveStaticFile("/offer/", form);
                data.Image = imagename;
            }
            await _offerRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Offer");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _offerRepository.GetByIdAsync(cancellationToken, Id);
            await _offerRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
