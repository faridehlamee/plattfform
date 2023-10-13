using Data.DTO.BaseDTO;
using Entites.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Common
{
    public class PricingDTO : BaseDto<PricingDTO, Pricing, int>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public string Unit { get; set; }
        public string Options { get; set; }
        public List<string> ListOption {
            get
            {
                try
                {
                    return Options.Split(',').ToList();
                }
                catch (Exception)
                {

                    return new List<string>();
                }


            }
        }
    }
}
