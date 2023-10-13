using Data.DTO.BaseDTO;
using Entites.Entities.Movies;
using Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Movies
{
    public class MovieCategoryDTO : BaseDto<MovieCategoryDTO, MovieCategory, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public MovieDTO Movie { get; set; }
        public List<MovieDTO> ListMovie { get; set; }
    }
}
