using AutoMapper;
using AutoMapper.QueryableExtensions; 
using Data.Contracts.Common;
using Data.Contracts.Movie;
using Data.DTO.Movies;
using Data.DTO.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DashBoard.Controllers.Movie
{
    public class MovieController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMovieCategoryRepository  _movieCategoryRepository;
        private readonly IMovieRepository  _movieRepository;

        public MovieController(IMapper mapper, IMovieCategoryRepository  movieCategoryRepository
            , IMovieRepository  movieRepository)
        {
            this._mapper = mapper;
            this._movieCategoryRepository = movieCategoryRepository;
            this._movieRepository = movieRepository;
        }
        public IActionResult Index() { return View(); }
        public async Task<JsonResult> ListAsync(SearchDTO model, MovieDTO Search, CancellationToken cancellationToken)
        {

            var dto = await _movieRepository.GetPaging(model, Search);
            return Json(new { total = dto.TotalProduct, data = dto.Resualt });
        }


        public async Task<IActionResult> Create()
        {
            var data = new MovieDTO();
            data.ListMovieCategory = await _movieCategoryRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
                .ToListAsync();
            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(MovieDTO model)
        {
            var data = model.ToEntity(_mapper);
            
            await _movieRepository.AddAsync(data, CancellationToken.None);
            return RedirectToAction("Index", "Movie");

        }
        public async Task<IActionResult> Editpage(int Id)
        {
            var data = await _movieRepository.TableNoTracking.ProjectTo<MovieDTO>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync(p => p.Id == Id, CancellationToken.None);

            data.ListMovieCategory = await _movieCategoryRepository.TableNoTracking.Where(c => c.IsActive)
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Title
                })
                .ToListAsync();



            return View(data);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(MovieDTO model, CancellationToken cancellationToken)
        {
            var data = await _movieRepository.GetByIdAsync(cancellationToken, model.Id);
            data = model.ToEntity(_mapper, data);
           
            await _movieRepository.UpdateAsync(data, cancellationToken);
            return RedirectToAction("Index", "Movie");
        }
        public async Task<JsonResult> Delete(int Id, CancellationToken cancellationToken)
        {
            var data = _movieRepository.GetByIdAsync(cancellationToken, Id);
            await _movieRepository.DeleteIsActiveAsync(data.Result, cancellationToken);
            return Json(true);
        }
    }
}
