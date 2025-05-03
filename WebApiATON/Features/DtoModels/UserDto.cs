

using System.ComponentModel.DataAnnotations;

namespace WebApiATON.Features.DtoModels
{
    public record UserDto
    {
        [Required(ErrorMessage = "A login is required")]
        [MaxLength(255, ErrorMessage = "The maximum length of a login is 255 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "The login must contain only Latin letters and numbers.")]
        public string Login { get; set; } = null!;

        [Required(ErrorMessage = "A password is required")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
            ErrorMessage = "The password must contain only Latin letters and numbers.")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "A name is required")]
        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 characters.")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ]+$",
            ErrorMessage = "The name must contain only Russian and Latin letters.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Gender is required")]
        [Range(0, 2, ErrorMessage = "Gender should be 0 (female), 1 (male) or 2 (unknown)")]
        public int Gender { get; set; }

        public DateTime? Birthday { get; set; }

        public bool Admin { get; set; } = false;
    }


    public record UserInfoDto
    {
        public string Name { get; set; } = null!;
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }

        public bool IsActive { get; set; }
    }
}
