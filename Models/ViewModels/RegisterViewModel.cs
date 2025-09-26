using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class RegisterViewModel
    {
        [Required, MaxLength(100)]
        public string FirstName { get; set; }

        [Required, MaxLength(100)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        // Volunteer-specific
        public bool IsVolunteer { get; set; }
        public string Skills { get; set; }
        public string Availability { get; set; }
    }
}
