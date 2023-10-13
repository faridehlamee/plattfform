using Data.Contracts.Blog;
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

namespace UI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository  _blogRepository;
        private readonly IBlogCategoryRepository _blogCategoryRepository;

        public BlogController(IBlogRepository  blogRepository, IBlogCategoryRepository  blogCategoryRepository)
        {
            this._blogRepository = blogRepository;
            this._blogCategoryRepository = blogCategoryRepository;
        }
        public async Task<IActionResult> Index(int? blogCategoryId)
        { 
            var viewModel = new BlogDTO();
            if (blogCategoryId != null)
            {
                viewModel.BlogCategoryId = blogCategoryId.Value;
                viewModel.BlogCategoryName = _blogCategoryRepository.GetById(blogCategoryId).Name;


            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            viewModel.ListBlogCategory = await _blogCategoryRepository.TableNoTracking.Where(c => c.IsActive)
                                             .Select(s => new SelectListItem
                                             {
                                                 Value = s.Id.ToString(),
                                                 Text = s.Name
                                             })
                                             .ToListAsync();


            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> GetListBlog(SearchDTO model, BlogDTO course)
        {
            var data = await _blogRepository.GetPaging(model, course);
            return Json(data);
        }

        public async Task<IActionResult> BlogDetail(int id, CancellationToken cancellationToken)
        {
            var course = await _blogRepository.TableNoTracking.Where(c=> c.Id==id).Select(c=> new BlogDTO { 
            BlogCategoryId=c.BlogCategoryId,
            BlogCategoryName=c.BlogCategory.Name,
            Title=c.Title,
            DateInsert=c.DateInsert,
            Image=c.Image,
            Id=c.Id,
            Sumary=c.Sumary,
            Description=c.Description,
            AparatID=c.AparatID,
            AparatLink=c.AparatLink
            
            }).FirstOrDefaultAsync();
            return View(course);
        }
    }
}
