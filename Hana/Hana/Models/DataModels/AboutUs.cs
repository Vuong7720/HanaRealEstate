using System.ComponentModel.DataAnnotations;

namespace Hana.Models.DataModels
{
    public partial class AboutUs
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string? Content { get; set; }
    }
}
