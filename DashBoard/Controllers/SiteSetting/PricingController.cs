using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.Common;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.SiteSetting
{
    public class PricingController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Pricing> _pricingRepository;

        public PricingController(IMapper mapper, IRepository<Pricing> pricingRepository)
        {
            this._mapper = mapper;
            this._pricingRepository = pricingRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _pricingRepository.TableNoTracking.ProjectTo<PricingDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PricingDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _pricingRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Pricing");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _pricingRepository.TableNoTracking.ProjectTo<PricingDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(PricingDTO model, CancellationToken cancellationToken)
        {
            var data = await _pricingRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _pricingRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Pricing");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _pricingRepository.GetByIdAsync(cancellationToken, Id);
            await _pricingRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
