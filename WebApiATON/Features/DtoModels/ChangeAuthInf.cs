

using System.ComponentModel.DataAnnotations;

namespace WebApiATON.Features.DtoModels
{
    public record ChangePasswordRequest
    {
        public string? Login { get; set; } // Только для администратора

        [Required(ErrorMessage = "A password is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "The password must contain only Latin letters and numbers.")]
        public string NewPassword { get; set; } = string.Empty;
    }

    public record ChangeLoginRequest
    {
        public string? Login { get; set; } // Только для администратора

        [MaxLength(255, ErrorMessage = "The maximum length of a login is 255 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "The login must contain only Latin letters and numbers.")]
        public string NewLogin { get; set; } = string.Empty;

    }
}
