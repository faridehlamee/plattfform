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
    public class ServiceController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Service> _serviceRepository;

        public ServiceController(IMapper mapper, IRepository<Service> serviceRepository)
        {
            this._mapper = mapper;
            this._serviceRepository = serviceRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _serviceRepository.TableNoTracking.ProjectTo<ServiceDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(ServiceDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _serviceRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Service");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _serviceRepository.TableNoTracking.ProjectTo<ServiceDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(ServiceDTO model, CancellationToken cancellationToken)
        {
            var data = await _serviceRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _serviceRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Service");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _serviceRepository.GetByIdAsync(cancellationToken, Id);
            await _serviceRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
