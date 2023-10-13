using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Common.AllEnum.Commons;

namespace Entites.Entities
{
    public class OfferItem : BaseEntity<int>, IEntity<int>
    {
        public int? OfferId { get; set; }
        public virtual Offer.Offer Offer { get; set; }

        public int? ProductId { get; set; }
        public virtual Product.Product Product { get; set; }

    }
}
