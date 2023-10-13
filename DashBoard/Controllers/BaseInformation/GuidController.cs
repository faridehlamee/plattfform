using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.BaseProduct;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers.BaseInformation
{
    [Authorize(Roles = "Admin")]
    public class GuidController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<Guide> _guideRepository;
        private readonly IRepository<StoreType> _storetypeRepository;

        public GuidController(IMapper Mapper, IImageRepository ImageRepository, IRepository<Guide> guideRepository, IRepository<StoreType> storetypeRepository)
        {
            _mapper = Mapper;
            _imageRepository = ImageRepository;
            _guideRepository = guideRepository;
            _storetypeRepository = storetypeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _guideRepository.TableNoTracking.ProjectTo<GuideDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync(cancellationToken);

            return Json(dto);
        }

        public async Task<IActionResult> Create() {
            var data = new GuideDTO();
            data.ListStoreType = await _storetypeRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StoreName
                })
                .ToListAsync();
            return View(data); 
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GuideDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/sizeguid/", form);
                data.ImageFile = imagename;
            }
            data.Description = model.Title;
            await _guideRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Guid");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _guideRepository.TableNoTracking.ProjectTo<GuideDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            data.ListStoreType = await _storetypeRepository.TableNoTracking.Where(c => c.IsActive)
               .Select(s => new SelectListItem
               {
                   Value = s.Id.ToString(),
                   Text = s.StoreName
               })
               .ToListAsync();
            data.Title = data.Description;
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(GuideDTO model, CancellationToken cancellationToken)
        {
            var data = await _guideRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.ImageFile != null)
                {
                    _imageRepository.DeleteStaticImage(data.ImageFile, "/guid/");
                }
                var imagename = _imageRepository.SaveStaticFile("/guid/", form);
                data.ImageFile = imagename;
            }
            data.Description = model.Title;
            await _guideRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Guid");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _guideRepository.GetByIdAsync(cancellationToken, Id);
            await _guideRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
