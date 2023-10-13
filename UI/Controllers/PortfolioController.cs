using Data.Contracts;
using Data.DTO.Common;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class PortfolioController : Controller
    {
        private readonly IRepository<Portfolio> _portfolioRepository;

        public PortfolioController(IRepository<Portfolio> portfolioRepository)
        {
            this._portfolioRepository = portfolioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _portfolioRepository.TableNoTracking.Where(c => c.IsActive).Select(c => new PortfolioDTO { 
            Id=c.Id,
            Category=c.Category,
            Name=c.Name,
            CompletionTime=c.CompletionTime,
            ProjectDate=c.ProjectDate,
            Url=c.Url,
            
            }).ToListAsync();
            return View(data);
        }
    }
}
