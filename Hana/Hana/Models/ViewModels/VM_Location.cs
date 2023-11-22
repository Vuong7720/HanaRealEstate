namespace Hana.Models.ViewModels
{
    public class VM_Location
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public decimal Price { get; set; }
        public decimal? Latitude { get; set; }
        public decimal? Longtitude { get; set; }
    }
}
