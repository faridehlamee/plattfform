﻿using Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities
{
    public class OfferType : BaseEntity<int>, IEntity<int>
    {
        public string Type { get; set; }
    }
}
