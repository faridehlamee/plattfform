using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.Blog; 
using Data.Contracts.Common;
using Data.DTO.Blog;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Blog
{
    public class BlogController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IBlogCategoryRepository _blogCategoryRepository;
        private readonly IBlogRepository _blogRepository;
        private readonly IImageRepository _imageRepository;

        public BlogController(IMapper mapper, IBlogCategoryRepository blogCategoryRepository
            , IBlogRepository blogRepository
            ,IImageRepository imageRepository)
        {
            this._mapper = mapper;
            this._blogCategoryRepository = blogCategoryRepository;
            this._blogRepository = blogRepository;
            this._imageRepository = imageRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model, BlogDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _blogRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }


        public async Task<IActionResult> Create()
        {
            var data = new BlogDTO();
            data.ListBlogCategory = await _blogCategoryRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(BlogDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/blog/", form);
                data.Image = imagename;
            }
            await _blogRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Blog");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _blogRepository.TableNoTracking.ProjectTo<BlogDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            data.ListBlogCategory = await _blogCategoryRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name
                })
                .ToListAsync();

            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(BlogDTO model, CancellationToken cancellationToken)
        {
            var data = await _blogRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/blog/");
                }
                var imagename = _imageRepository.SaveStaticFile("/blog/", form);
                data.Image = imagename;
            }
            await _blogRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Blog");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _blogRepository.GetByIdAsync(cancellationToken, Id);
            await _blogRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
