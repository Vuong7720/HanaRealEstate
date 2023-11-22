using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class Rating
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public int RealEstateId { get; set; }

        public virtual RealEstate? RealEstate { get; set; }
    }
}
