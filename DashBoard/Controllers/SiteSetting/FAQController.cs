using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.Common;
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

namespace DashBoard.Controllers.SiteSetting
{
    [Authorize(Roles = "Admin")]
    public class FAQController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<FAQ> _faqRepository;

        public FAQController(IMapper Mapper, IRepository<FAQ> FAQRepository)
        {
            _mapper = Mapper;
            _faqRepository = FAQRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _faqRepository.TableNoTracking.ProjectTo<FAQDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(FAQDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _faqRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "FAQ");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _faqRepository.TableNoTracking.ProjectTo<FAQDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(FAQDTO model, CancellationToken cancellationToken)
        {
            var data = await _faqRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _faqRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "FAQ");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _faqRepository.GetByIdAsync(cancellationToken, Id);
            await _faqRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
