using Data.Contracts;
using Data.DTO.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IRepository<Entites.Entities.Service> _serviceRepository;

        public ServiceController(
             IRepository<Entites.Entities.Service> serviceRepository)
        {
            this._serviceRepository = serviceRepository;
        }
        public async Task<IActionResult> Index()
        {

           var Services = await _serviceRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new ServiceDTO
            {

                Id = c.Id,
                Name = c.Name,
                Icone = c.Icone,
                Decription = c.Decription
            }).ToListAsync();

            return View(Services);
        }
    }
}
