using System.ComponentModel.DataAnnotations;

namespace Hana.Models.ViewModels
{
    public class VM_ChangePassword
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Mật khẩu chứa tối thiểu 6 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Mật khẩu xác nhận không khớp!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
