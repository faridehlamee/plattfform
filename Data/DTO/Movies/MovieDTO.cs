using Data.DTO.BaseDTO;
using Entites.Entities.Movies;
using Entities.Common;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Movies
{
    public class MovieDTO : BaseDto<MovieDTO, Movie, int>
    {
        public int MovieCategoryId { get; set; }
        public string MovieCategoryTitle { get; set; }
        //public MovieCategoryDTO MovieCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AparatID { get; set; }
        public string AparatLink { get; set; }
        public int NumberShow { get; set; }
        public int Rates { get; set; }
        public List<SelectListItem> ListMovieCategory { get; set; }
    }
}
