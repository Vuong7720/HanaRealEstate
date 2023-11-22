using System.ComponentModel.DataAnnotations;

namespace Hana.Models.ViewModels
{
    public class VM_VerifyPhoneNumber
    {
        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Số điện thoại không hợp lệ 1")]
        [StringLength(12, ErrorMessage = "Số điện thoại không hợp lệ 2.", MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Mã xác nhận không được để trống")]
        public string VerificationCode { get; set; }
    }
}
