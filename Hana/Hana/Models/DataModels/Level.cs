using System;
using System.Collections.Generic;

namespace Hana.Models.DataModels
{
    public partial class Level
    {
        public Level()
        {
            Agent = new HashSet<Agent>();
        }

        public int Id { get; set; }
        public string? LevelName { get; set; }

        public virtual ICollection<Agent>? Agent { get; set; }
    }
}
