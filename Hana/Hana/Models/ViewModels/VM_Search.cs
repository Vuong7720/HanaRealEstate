using System.Collections.Generic;

namespace Hana.Models.ViewModels
{
    public class VM_Search
    {
        public int Type { get; set; }
        public int City { get; set; }
        public int District { get; set; }

        public int PriceRange { get; set; }

        public int AcreageRange { get; set; }
        public string? SearchString { get; set; }
    }

    public class VM_Search_Result
    {
        public int Id { get; set; }

        public string? Street { get; set; }

        public decimal Price { get; set; }

        public int Acreage { get; set; }
        public int? Type { get; set; }

        public string? PostTime { get; set; }

        public string? ImageUrl { get; set; }

        public string? AgentName { get; set; }
    }
    public class VM_Search_Result_Container
    {
        public VM_Search? SearchObject { get; set; }

        public IEnumerable<VM_Search_Result>? ResultList { get; set; }
    }
}
