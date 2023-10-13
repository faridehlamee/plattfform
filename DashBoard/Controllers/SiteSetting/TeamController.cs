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
    public class TeamController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Team> _teamRepository;
        private readonly IImageRepository _imageRepository;

        public TeamController(IMapper mapper, IRepository<Team> teamRepository, IImageRepository imageRepository)
        {
            this._mapper = mapper;
            this._teamRepository = teamRepository;
            this._imageRepository = imageRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _teamRepository.TableNoTracking.ProjectTo<TeamDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TeamDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/team/", form);
                data.Image = imagename;
            }
            await _teamRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Team");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _teamRepository.TableNoTracking.ProjectTo<TeamDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TeamDTO model, CancellationToken cancellationToken)
        {
            var data = await _teamRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/team/");
                }
                var imagename = _imageRepository.SaveStaticFile("/team/", form);
                data.Image = imagename;
            }
            await _teamRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Team");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _teamRepository.GetByIdAsync(cancellationToken, Id);
            await _teamRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
