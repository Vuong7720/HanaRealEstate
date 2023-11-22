using System.Collections.Generic;

namespace Hana.Models.ViewModels
{
    public class Filter
    {
        public Condition Condition { get; set; }

        public List<Result> Results { get; set; }
    }

    public class Condition
    {
        public string SearchString { get; set; }
        public int Type { get; set; }
        public int City { get; set; }
        public int District { get; set; }

        public int PriceRange { get; set; }

        public int AcreageRange { get; set; }
    }

    public class Result
    {
        public int Id { get; set; }

        public string? Street { get; set; }

        public decimal Price { get; set; }

        public long Acreage { get; set; }
        public int? Type { get; set; }

        public string? PostTime { get; set; }

        public string? ImageUrl { get; set; }

        public string? AgentName { get; set; }
        public string? ContactNumber { get; set; }
    }
}
