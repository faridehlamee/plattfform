using Data.DTO.BaseDTO;
using Entites.Entities.BaseProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.BaseProduct
{
    public class KeywordDTO : BaseDto<KeywordDTO, Keyword, int>
    {
        public string Key { get; set; }
    }
}
