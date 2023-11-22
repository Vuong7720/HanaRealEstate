using System.ComponentModel.DataAnnotations;

namespace Hana.Models.ViewModels
{
    public class VM_Login
    {
        [Required]
        public string? LoginName { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        public bool IsRememberMe { get; set; }

        public string? ReturnUrl { get; set; }
    }
}

