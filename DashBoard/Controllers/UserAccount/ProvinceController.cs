using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.Address;
using Data.Contracts;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.UserAccount
{
    [Authorize(Roles = "Admin")]
    public class ProvinceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Province> _provinceRepository;

        public ProvinceController(IMapper Mapper, IRepository<Province> ProvinceRepository)
        {
            _mapper = Mapper;
            _provinceRepository = ProvinceRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _provinceRepository.TableNoTracking.ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ProvinceDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _provinceRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Province");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _provinceRepository.TableNoTracking.ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ProvinceDTO model, CancellationToken cancellationToken)
        {
            var data = await _provinceRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _provinceRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Province");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _provinceRepository.GetByIdAsync(cancellationToken, Id);
            await _provinceRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
