using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.BaseInformation
{
    [Authorize(Roles = "Admin")]
    public class DetailController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Details> _detailsRepository;

        public DetailController(IMapper Mapper, IRepository<Details> DetailsRepository)
        {
            _mapper = Mapper;
            _detailsRepository = DetailsRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _detailsRepository.TableNoTracking.ProjectTo<DetailsDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DetailsDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _detailsRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Detail");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _detailsRepository.TableNoTracking.ProjectTo<DetailsDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(DetailsDTO model, CancellationToken cancellationToken)
        {
            var data = await _detailsRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _detailsRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Detail");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _detailsRepository.GetByIdAsync(cancellationToken, Id);
            await _detailsRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
