using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.BaseProduct;
using Data.Contracts;
using Entites.Entities;
using Entities.Entities.WareHouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.StructureStore
{
    [Authorize(Roles = "Admin")]
    public class StoreTypeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StoreType> _storeTypeRepository;
        private readonly IRepository<GenderProductType> _genderProductTypeRepository;
        private readonly IRepository<TypeSize> _typeSizeRepository;

        public StoreTypeController(IMapper Mapper, IRepository<StoreType> StoreTypeRepository, IRepository<GenderProductType> GenderProductTypeRepository, IRepository<TypeSize> TypeSizeRepository)
        {
            _mapper = Mapper;
            _storeTypeRepository = StoreTypeRepository;
            _genderProductTypeRepository = GenderProductTypeRepository;
            _typeSizeRepository = TypeSizeRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _storeTypeRepository.TableNoTracking.ProjectTo<StoreTypeDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        public async Task<IActionResult> Create()
        {
            var data = new StoreTypeDTO();
            data.GenderProductTypeList = await _genderProductTypeRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();
        

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(StoreTypeDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _storeTypeRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "StoreType");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _storeTypeRepository.TableNoTracking.ProjectTo<StoreTypeDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);
            data.GenderProductTypeList = await _genderProductTypeRepository.TableNoTracking.Where(c => c.IsActive)
             .Select(s => new SelectListItem
             {
                 Value = s.Id.ToString(),
                 Text = s.Name
             })
             .ToListAsync();
           

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(StoreTypeDTO model, CancellationToken cancellationToken)
        {
            var data = await _storeTypeRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            await _storeTypeRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "StoreType");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _storeTypeRepository.GetByIdAsync(cancellationToken, Id);
            await _storeTypeRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
