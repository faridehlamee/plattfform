using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.Offer
{
    public class Offer : BaseEntity<int>, IEntity<int>
    {
        public string Name { get; set; }
        public string description { get; set; }
        public string Image { get; set; }
        public DateTime ExpirDate { get; set; }

        public int OfferTypeId { get; set; }
        public OfferType OfferType { get; set; }
        public int OfferZoneId { get; set; }
        public OfferZone OfferZone { get; set; }
    }
}
