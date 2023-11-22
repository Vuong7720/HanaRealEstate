using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class RealEstateType
    {
        public RealEstateType()
        {
            RealEstate = new HashSet<RealEstate>();
        }

        public int Id { get; set; }
        public string RealEstateTypeName { get; set; }

        public virtual ICollection<RealEstate> RealEstate { get; set; }
    }
}
