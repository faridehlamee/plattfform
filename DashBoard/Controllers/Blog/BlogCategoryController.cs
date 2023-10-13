using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.Blog;
using Data.Contracts.Common;
using Data.DTO.Blog;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Blog
{
    public class BlogCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IImageRepository _imageRepository;

        public BlogCategoryController(IMapper mapper, IBlogCategoryRepository  blogCategoryRepository , IImageRepository imageRepository  )
        {
            _blogCategoryRepository = blogCategoryRepository;
            this._imageRepository = imageRepository;
            _mapper = mapper;
        }

        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model, BlogCategoryDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _blogCategoryRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlogCategoryDTO model)
        {
            var data = model.ToEntity(_mapper);
        
            await _blogCategoryRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "BlogCategory");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _blogCategoryRepository.TableNoTracking.ProjectTo<BlogCategoryDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(BlogCategoryDTO model, CancellationToken cancellationToken)
        {
            var data = await _blogCategoryRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);

       
            await _blogCategoryRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "BlogCategory");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _blogCategoryRepository.GetByIdAsync(cancellationToken, Id);
            await _blogCategoryRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
