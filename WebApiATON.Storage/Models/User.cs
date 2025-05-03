

using System.ComponentModel.DataAnnotations;

namespace WebApiATON.Storage.Models
{
    public class User
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required, MaxLength(255)]
        public string Login { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;
        [Required]
        public int Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public bool Admin { get; set; } = false;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; } = null!;
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? RevokedOn { get; set; }
        public string? RevokedBy { get; set; }
    }
}
