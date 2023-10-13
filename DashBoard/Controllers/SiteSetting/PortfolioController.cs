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
    public class PortfolioController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Portfolio> _portfolioRepository;
        private readonly IImageRepository _imageRepository;

        public PortfolioController(IMapper mapper, IRepository<Portfolio> portfolioRepository, IImageRepository imageRepository)
        {
            this._mapper = mapper;
            this._portfolioRepository = portfolioRepository;
            this._imageRepository = imageRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _portfolioRepository.TableNoTracking.ProjectTo<PortfolioDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(PortfolioDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/portfolio/", form);
                data.Image = imagename;
            }
            await _portfolioRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Portfolio");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _portfolioRepository.TableNoTracking.ProjectTo<PortfolioDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(PortfolioDTO model, CancellationToken cancellationToken)
        {
            var data = await _portfolioRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/portfolio/");
                }
                var imagename = _imageRepository.SaveStaticFile("/portfolio/", form);
                data.Image = imagename;
            }
            await _portfolioRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Portfolio");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _portfolioRepository.GetByIdAsync(cancellationToken, Id);
            await _portfolioRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
