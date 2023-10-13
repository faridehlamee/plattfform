using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Contracts.Common;
using Data.Contracts.Movie;
using Data.DTO.Movies;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Movie
{
    public class MovieCategoryController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieCategoryRepository _movieCategoryRepository;
        private readonly IImageRepository _imageRepository;

        public MovieCategoryController(IMapper mapper, IMovieCategoryRepository  movieCategoryRepository, IImageRepository imageRepository  )
        {
            _movieCategoryRepository = movieCategoryRepository;
            this._imageRepository = imageRepository;
            _mapper = mapper;
        }

        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model, MovieCategoryDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _movieCategoryRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }

        public IActionResult Create() { return View(); }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MovieCategoryDTO model)
        {
            var data = model.ToEntity(_mapper);
            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                var imagename = _imageRepository.SaveStaticFile("/movieCategory/", form);
                data.Image = imagename;
            }
            await _movieCategoryRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "MovieCategory");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _movieCategoryRepository.TableNoTracking.ProjectTo<MovieCategoryDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);


            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(MovieCategoryDTO model, CancellationToken cancellationToken)
        {
            var data = await _movieCategoryRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);

            var form = await Request.ReadFormAsync();
            if (form.Files.Count > 0)
            {
                if (data.Image != null)
                {
                    _imageRepository.DeleteStaticImage(data.Image, "/movieCategory/");
                }
                var imagename = _imageRepository.SaveStaticFile("/movieCategory/", form);
                data.Image = imagename;
            }

            await _movieCategoryRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "MovieCategory");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _movieCategoryRepository.GetByIdAsync(cancellationToken, Id);
            await _movieCategoryRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
