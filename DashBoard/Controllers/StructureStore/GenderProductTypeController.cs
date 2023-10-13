using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.Common;
using Data.DTO.BaseProduct;
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

namespace DashBoard.Controllers.StructureStore
{
    [Authorize(Roles = "Admin")]
    public class GenderProductTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IRepository<GenderProductType> _genderProductTypeRepository;

        public GenderProductTypeController(IMapper Mapper, IImageRepository ImageRepository, IRepository<GenderProductType> GenderProductTypeRepository)
        {
            _mapper = Mapper;
            _imageRepository = ImageRepository;
            _genderProductTypeRepository = GenderProductTypeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _genderProductTypeRepository.TableNoTracking.ProjectTo<GenderProductTypeDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenderProductTypeDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/genderProductType/", form);
                data.Image = imagename;
            }
            await _genderProductTypeRepository.AddAsync(data , CancellationToken.None);
            return RedirectToAction("Index", "GenderProductType");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _genderProductTypeRepository.TableNoTracking.ProjectTo<GenderProductTypeDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(GenderProductTypeDTO model, CancellationToken cancellationToken)
        {
            var data = await _genderProductTypeRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);

            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/genderProductType/");
                }
                var imagename = _imageRepository.SaveStaticFile("/genderProductType/", form);
                data.Image = imagename;
            }

            await _genderProductTypeRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "GenderProductType");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _genderProductTypeRepository.GetByIdAsync(cancellationToken, Id);
            await _genderProductTypeRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }

    }
}
