using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class Volunteer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }

        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}