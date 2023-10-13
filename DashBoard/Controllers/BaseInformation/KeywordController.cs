using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Data.Contracts;
using Data.DTO.BaseProduct;
using Entites.Entities.BaseProduct;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DashBoard.Controllers.BaseInformation
{
    [Authorize(Roles = "Admin")]
    public class KeywordController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Keyword> _keywordrepository;

        public KeywordController(IMapper mapper ,  IRepository<Keyword> keywordrepository)
        {
            _mapper = mapper;
            _keywordrepository = keywordrepository;
        }



        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(CancellationToken cancellationToken)
        {
            var dto = await _keywordrepository.TableNoTracking
                .Where(c => c.IsActive)
                .ToListAsync();

            return Json(dto);
        }

        [HttpPost]
        public JsonResult KeywordList()
        {
            var dto =  _keywordrepository.TableNoTracking
                   .Where(c => c.IsActive).Select(c=> c.Key)
                   .ToList();
            return Json(dto);
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(KeywordDTO model)
        {
            var data = model.ToEntity(_mapper);
            await _keywordrepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Keyword");

        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _keywordrepository.GetByIdAsync(cancellationToken, Id);
            await _keywordrepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
