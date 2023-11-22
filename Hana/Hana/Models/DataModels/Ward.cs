using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class Ward
    {
        public Ward()
        {
            Map = new HashSet<Map>();
        }

        public int Id { get; set; }
        public string WardName { get; set; }
        public int? DistrictId { get; set; }

        public virtual District District { get; set; }
        public virtual ICollection<Map> Map { get; set; }
    }
}
