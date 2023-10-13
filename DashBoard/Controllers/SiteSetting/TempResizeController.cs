using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.DTO.Common;
using Data.Contracts;
using Entites.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Data.Contracts.Common;
using Data.Contracts.Product;

namespace DashBoard.Controllers.SiteSetting
{
    [Authorize(Roles = "Admin")]
    public class TempResizeController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IImageRepository _imageRepository;
        private readonly IProductRepository _productRepository;

        public TempResizeController(IMapper Mapper, IImageRepository imageRepository , IProductRepository productRepository)
        {
            _mapper = Mapper;
            _imageRepository = imageRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index() { return View(); }
 
        [HttpPost]
        public async Task<IActionResult> CreateAsync(int fromId , int ToId)
        {
            var listName =await _productRepository.TableNoTracking.Where(c => c.Id >= fromId && c.Id <= ToId).Select(x=> x.CurrentImage).ToListAsync();

            await _imageRepository.TempResize(250, 250, listName);

            return RedirectToAction("Index", "TempResize");

        }
    }
}
