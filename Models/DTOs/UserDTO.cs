using System.ComponentModel.DataAnnotations;

namespace Home.Models.DTOs
{
    public class UserTokenDTO
    {
        public string? Token { get; set; }
        public DateTime? ExpiresIn { get; set; }
    }

    public class UserAccessDTO
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Email { get; set; } = null!;

        [Display(Name = "Password")]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string Password { get; set; } = null!;
    }

    public class UserSignUpEditorDTO : UserAccessDTO
    {
    }

    public class UserLogInEditorDTO : UserAccessDTO
    {
    }

    public class UserChangePasswordEditorDTO
    {
        [Display(Name = "Current passwoed")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string CurrentPassword { get; set; } = null!;

        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "This field is mandatory")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "This field must be between {2} and {1} characters")]
        public string NewPassword { get; set; } = null!;
    }
}
