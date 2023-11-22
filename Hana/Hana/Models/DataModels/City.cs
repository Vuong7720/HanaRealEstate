using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class City
    {
        public City()
        {
            District = new HashSet<District>();
            Map = new HashSet<Map>();
        }

        public int Id { get; set; }
        public string CityName { get; set; }

        public virtual ICollection<District>? District { get; set; }
        public virtual ICollection<Map>? Map { get; set; }
    }
}
