using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.Address;
using Data.Contracts;
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

namespace DashBoard.Controllers.UserAccount
{
    [Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Province> _provinceRepository;

        public CityController(IMapper Mapper , IRepository<City> CityRepository , IRepository<Province> ProvinceRepository)
        {
            _mapper = Mapper;
            _cityRepository = CityRepository;
            _provinceRepository = ProvinceRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _cityRepository.TableNoTracking.ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create()
        {
            var data = new CityDTO();
            data.ListProvince = await _provinceRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CityDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _cityRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "City");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _cityRepository.TableNoTracking.ProjectTo<CityDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            data.ListProvince = await _provinceRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(CityDTO model, CancellationToken cancellationToken)
        {
            var data = await _cityRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _cityRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "City");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _cityRepository.GetByIdAsync(cancellationToken, Id);
            await _cityRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
