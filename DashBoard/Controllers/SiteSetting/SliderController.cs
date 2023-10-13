using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.Common;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.SiteSetting
{
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<Slider> _sliderRepository;

        public SliderController(IMapper Mapper, IImageRepository ImageRepository, IRepository<Slider> SliderRepository)
        {
            _mapper = Mapper;
            _imageRepository = ImageRepository;
            _sliderRepository = SliderRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _sliderRepository.TableNoTracking.ProjectTo<SliderDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SliderDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/slider/", form);
                data.Image = imagename;
            }
            await _sliderRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Slider");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _sliderRepository.TableNoTracking.ProjectTo<SliderDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(SliderDTO model, CancellationToken cancellationToken)
        {
            var data = await _sliderRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/slider/");
                }
                var imagename = _imageRepository.SaveStaticFile("/slider/", form);
                data.Image = imagename;
            }
            await _sliderRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Slider");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _sliderRepository.GetByIdAsync(cancellationToken, Id);
            await _sliderRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
