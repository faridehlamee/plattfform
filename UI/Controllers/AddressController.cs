using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Data.Contracts;
using Data.Contracts.User;
using Data.DTO.Address;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UI.Controllers
{
    public class AddressController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressController(IMapper Mapper, IRepository<City> CityRepository,
            IRepository<Province> ProvinceRepository, IAddressRepository addressRepository)
        {
            _mapper = Mapper;
            _cityRepository = CityRepository;
            _provinceRepository = ProvinceRepository;
            _addressRepository = addressRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

     
        public async Task<JsonResult> GetProvince()
        {
            var data = await _provinceRepository.TableNoTracking
                .Where(c => c.IsActive).ProjectTo<ProvinceDTO>(_mapper.ConfigurationProvider).Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToListAsync();

            return Json(data);
        }


        public async Task<JsonResult> GetCity(int ProvinceId)
        {
            var data = await _cityRepository.TableNoTracking
               .Where(c => c.IsActive && c.ProvinceId == ProvinceId).ProjectTo<CityDTO>(_mapper.ConfigurationProvider).Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.Name
               }).ToListAsync();

            return Json(data);
        }


        [Authorize(Roles = "Client")]
        public async Task<JsonResult> AddAddress(string strAddress, CancellationToken cancellationToken)
        {
            AddressDTO address = JsonConvert.DeserializeObject<AddressDTO>(strAddress);
            await _addressRepository.CreateAndUpdateAddress(address, cancellationToken);
            return Json(true);
        }


        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _addressRepository.GetByIdAsync(cancellationToken, Id);
            await _addressRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
