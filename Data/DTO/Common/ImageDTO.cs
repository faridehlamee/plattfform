using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class ImageDTO : BaseDto<ImageDTO, Image, int>
    {
        public string ImageFile { get; set; }
        public int? EntityId { get; set; }
        public string EntityType { get; set; }
        public int Priority { get; set; }
    }
}
