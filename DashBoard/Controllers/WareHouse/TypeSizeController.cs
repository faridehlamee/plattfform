using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.WareHouse;
using Data.Contracts;
using Entities.Entities.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.WareHouse
{
    [Authorize(Roles = "Admin")]
    public class TypeSizeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<TypeSize> _typeSizeRepository;

        public TypeSizeController(IMapper Mapper, IRepository<TypeSize> TypeSizeRepository)
        {
            _mapper = Mapper;
            _typeSizeRepository = TypeSizeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _typeSizeRepository.TableNoTracking.ProjectTo<TypeSizeDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TypeSizeDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _typeSizeRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "TypeSize");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _typeSizeRepository.TableNoTracking.ProjectTo<TypeSizeDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(TypeSizeDTO model, CancellationToken cancellationToken)
        {
            var data = await _typeSizeRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _typeSizeRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "TypeSize");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _typeSizeRepository.GetByIdAsync(cancellationToken, Id);
            await _typeSizeRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
