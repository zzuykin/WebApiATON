
using System.ComponentModel.DataAnnotations;

namespace WebApiATON.Features.DtoModels
{
    public record UpdateUserRequest
    {
        [MaxLength(255, ErrorMessage = "The maximum length of a login is 255 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9]+$",
           ErrorMessage = "The login must contain only Latin letters and numbers.")]
        public string? Login { get; set; } // Только для админа


        [MaxLength(50, ErrorMessage = "The maximum length of a name is 50 characters.")]
        [RegularExpression(@"^[a-zA-Zа-яА-ЯёЁ]+$",
            ErrorMessage = "The name must contain only Russian and Latin letters.")]
        public string? NewName { get; set; }


        [Range(0, 2, ErrorMessage = "Gender should be 0 (female), 1 (male) or 2 (unknown)")]
        public int? NewGender { get; set; }

        public DateTime? NewBirthday { get; set; }
    }
}
