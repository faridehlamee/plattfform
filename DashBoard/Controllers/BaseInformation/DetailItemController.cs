using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.BaseInformation
{
    [Authorize(Roles = "Admin")]
    public class DetailItemController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<DetailsItem> _detailsItemRepository;
        private readonly IRepository<Details> _detailsRepository;

        public DetailItemController(IMapper Mapper, IRepository<DetailsItem> DetailsItemRepository, IRepository<Details> DetailsRepository)
        {
            _mapper = Mapper;
            _detailsItemRepository = DetailsItemRepository;
            _detailsRepository = DetailsRepository;
        }
        public IActionResult Index(int Id) 
        {
            var data = _detailsRepository.GetByIdAsync(CancellationToken.None, Id).Result;
            return View(data); 
        }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken , int Id)
        {
            var dto = await _detailsItemRepository.TableNoTracking.ProjectTo<DetailsItemDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive && c.DetailsId== Id)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create(int DetailId) 
        {
            var data = new DetailsItemDTO();
            data.DetailsId = DetailId;
          
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DetailsItemDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _detailsItemRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "DetailItem",new { Id=data.DetailsId});

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _detailsItemRepository.TableNoTracking.ProjectTo<DetailsItemDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(DetailsItemDTO model, CancellationToken cancellationToken)
        {
            var data = await _detailsItemRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _detailsItemRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "DetailItem", new { Id = data.DetailsId });
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _detailsItemRepository.GetByIdAsync(cancellationToken, Id);
            await _detailsItemRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
