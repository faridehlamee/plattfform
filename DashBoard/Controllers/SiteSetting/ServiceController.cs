using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.Contracts.Common;
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
        private readonly IImageRepository _imageRepository;

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
            var form = await Request.ReadFormAsync();
           // await _serviceRepository.AddAsync(data, CancellationToken.None);
          



          
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/service/", form);
                data.Image = imagename;
            }
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
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "~/service/");
                }
                var imagename = _imageRepository.SaveStaticFile("~/service/", form);
                data.Image = imagename;
            }
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
