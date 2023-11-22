using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class District
    {
        public District()
        {
            Map = new HashSet<Map>();
            Ward = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public string DistrictName { get; set; }
        public int? CityId { get; set; }

        public virtual City? City { get; set; }
        public virtual ICollection<Map>? Map { get; set; }
        public virtual ICollection<Ward>? Ward { get; set; }
    }
}
