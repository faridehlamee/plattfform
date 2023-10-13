using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Data.Repositories;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Product
{
    [Authorize(Roles = "Admin")]
    public class AllocateToParameterController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<StoreType> _storeTypeRrepository;
        private readonly IRepository<StoreTypeDetail> _storeTypeDetailRepository;
        private readonly IRepository<Details> _detailsRepository;

        public AllocateToParameterController(IMapper Mapper ,IRepository<StoreType> StoreTypeRrepository  , IRepository<StoreTypeDetail> StoreTypeDetailRepository, IRepository<Details> DetailsRepository)
        {
            _mapper = Mapper;
            _storeTypeRrepository = StoreTypeRrepository;
            _storeTypeDetailRepository = StoreTypeDetailRepository;
            _detailsRepository = DetailsRepository;
        }
        public async Task<IActionResult> Index(int Id)
        {
            var model = new DetailsItemDTO()
            {
                DetailsId = Id,
                Details = await _detailsRepository.TableNoTracking.ProjectTo<DetailsDTO>(_mapper.ConfigurationProvider)
                .Where(c => c.IsActive  && c.Id==Id )
                .FirstOrDefaultAsync(),
            StoreTypeList = await _storeTypeRrepository.TableNoTracking.Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StoreName
                }).ToListAsync(),
                StoreTypeDetailList = await _storeTypeDetailRepository.TableNoTracking.Where(c=> c.DetailsId==Id).Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StoreTypeId.ToString()
                }).ToListAsync(),


            };
            return View(model);
        }
        public async Task<JsonResult> SaveRole(int ID, int DetailID, bool resualt)
        {
            if (resualt)
            {
                var data = new StoreTypeDetail()
                {
                    StoreTypeId = ID,
                    DetailsId = DetailID
                };
                await _storeTypeDetailRepository.AddAsync(data, CancellationToken.None);
                return Json(1);
            }
            else
            {
                var data = await _storeTypeDetailRepository.TableNoTracking.Where(c => c.DetailsId == DetailID && c.StoreTypeId == ID).FirstOrDefaultAsync();
                await _storeTypeDetailRepository.DeleteAsync(data, CancellationToken.None);
                return Json(0);
            }


        }
    }
}
