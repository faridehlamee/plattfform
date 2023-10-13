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
    public class DetailsItemDTO : BaseDto<DetailsItemDTO, DetailsItem, int>
    {
        public string SubTopic { get; set; }
        public int DetailsId { get; set; }
        public string DetailsTitle { get; set; }

        public virtual DetailsDTO Details { get; set; }
        public List<SelectListItem> DetailsList { get; set; }
        public List<SelectListItem> StoreTypeList { get; set; }
        public List<SelectListItem> StoreTypeDetailList { get; set; }



    }
}
