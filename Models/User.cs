using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace UserRegistration.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, DisplayName("Full Name")]
        public string? FullName { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required, DataType(DataType.Date), DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string? State { get; set; }

        [Required]
        public string? District { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; }

        [Required, Phone]
        public string? Phone { get; set; }

        [Required, DisplayName("Username")]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }

        public enum States
        {
            Kerala,
            Tamilnadu,
            Karnataka
        }
    }
}

