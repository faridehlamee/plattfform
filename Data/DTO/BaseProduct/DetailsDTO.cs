using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class DetailsDTO : BaseDto<DetailsDTO, Details, int>
    {
        public string Title { get; set; }
        public ICollection<DetailsItemDTO> ListDetailsItem { get; set; }
    }
}
