using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class StoreTypeDetailDTO : BaseDto<StoreTypeDetailDTO, StoreTypeDetail, int>
    {
        public int StoreTypeId { get; set; }
        public virtual StoreTypeDTO StoreType { get; set; }

        public int DetailsId { get; set; }
        public virtual DetailsDTO Details { get; set; }
    }
}
