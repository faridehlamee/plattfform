using Data.Contracts.Blog;
using Data.Contracts.Movie;
using Data.DTO.Blog;
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

namespace UI.Controllers
{
    public class EducationalFilmController : Controller
    {
        private readonly IMovieRepository   _movieRepository;
        private readonly IMovieCategoryRepository  _movieCategoryRepository;

        public EducationalFilmController(IMovieRepository  movieRepository, IMovieCategoryRepository  movieCategoryRepository)
        {
            this._movieRepository = movieRepository;
            this._movieCategoryRepository = movieCategoryRepository;
        }
        public async Task<IActionResult> Index(int? movieCategoryId)
        {
            var viewModel = new MovieDTO();
            //if (movieCategoryId != null)
            //    viewModel.MovieCategoryId = movieCategoryId.Value;

            //viewModel.ListMovieCategory = await _movieCategoryRepository.TableNoTracking.Where(c => c.IsActive)
            //                                 .Select(s => new SelectListItem
            //                                 {
            //                                     Value = s.Id.ToString(),
            //                                     Text = s.Title
            //                                 })
            //                                 .ToListAsync();


            return View(viewModel);
        }

        [HttpPost]
        public async Task<JsonResult> GetListMovie(SearchDTO model, MovieCategoryDTO movieCategory)
        {
            var data = await _movieCategoryRepository.GetPaging(model, movieCategory);
            return Json(data);
        }

        public async Task<IActionResult> MovieDetail(int id, int? movieId ,CancellationToken cancellationToken)
        {
           var course = await _movieCategoryRepository.TableNoTracking.Where(c=> c.Id==id).Select(c=> new MovieCategoryDTO { 
           Id=c.Id,
           Title=c.Title,
           Description=c.Description,
           Image=c.Image,
           DateInsert=c.DateInsert
           
           }).FirstOrDefaultAsync();

            course.ListMovie = await _movieRepository.TableNoTracking.Where(c => c.IsActive && c.MovieCategoryId == course.Id).Select(x => new MovieDTO
            {
                Id=x.Id,
                MovieCategoryId=x.MovieCategoryId,
                AparatID = x.AparatID,
                AparatLink = x.AparatLink,
                Name=x.Name

            }).ToListAsync();

            if(course.ListMovie.Count >0 && movieId !=null && movieId !=0)
                course.Movie = course.ListMovie.Where(c => c.Id == movieId.Value).FirstOrDefault();

            return View(course);
        }
    }
}
