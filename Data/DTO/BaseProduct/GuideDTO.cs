using Data.DTO.BaseDTO;
using Entites.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class GuideDTO : BaseDto<GuideDTO, Guide, int>
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public string ImageFile { get; set; }

        public int? StoreTypeId { get; set; }
        public string StoreTypeStoreName { get; set; }
        public List<SelectListItem> ListStoreType { get; set; }
        //public virtual StoreTypeDTO StoreType { get; set; }
    }
}
