using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class PortfolioDTO : BaseDto<PortfolioDTO, Portfolio, int>
    {
        public string Image { get; set; }
        public string Category { get; set; }
        public string Name { get; set; }
        public string CompletionTime { get; set; }
        public DateTime ProjectDate { get; set; }
        public string Url { get; set; }
        public string Detail { get; set; }
    }
}
