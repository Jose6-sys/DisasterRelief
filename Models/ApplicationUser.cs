using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FirstName { get; set; }

        [MaxLength(100)]
        public string? LastName { get; set; }

        // Volunteer-specific fields
        public bool IsVolunteer { get; set; }
        public string? Skills { get; set; }
        public string? Availability { get; set; }
    }
}
