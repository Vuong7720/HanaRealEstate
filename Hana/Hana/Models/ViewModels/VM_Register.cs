using System.ComponentModel.DataAnnotations;

namespace Hana.Models.ViewModels
{
    public class VM_Register
    {
        [Required(ErrorMessage ="Tài khoản không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Tài khoản đăng nhập phải tối thiểu 4 kí tự.", MinimumLength = 4)]
        [RegularExpression(@"^[^\s\,]+$", ErrorMessage = "Tên đăng nhập không được có khoảng trắng")]
        public string LoginName { get; set; }

        [Required(ErrorMessage ="Số điện thoại không được để trống")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại không hợp lệ 1")]
        [StringLength(12, ErrorMessage = "Số điện thoại không hợp lệ 2.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không hợp lệ")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Địa chỉ Email không hợp lệ")]
        public string Email { get; set; }


        [Required(ErrorMessage ="Họ tên không được để trống")]
        public string AgentName { get; set; }

        [Required(ErrorMessage ="Mật khẩu không được để trống")]
        [StringLength(100, ErrorMessage = "Mật khẩu chứa tối thiểu 6 kí tự.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp!")]
        public string? ConfirmPassword { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
