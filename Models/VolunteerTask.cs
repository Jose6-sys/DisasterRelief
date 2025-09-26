using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class VolunteerTask
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        //  Make volunteer optional
        public string? AssignedVolunteerId { get; set; }
        public ApplicationUser? AssignedVolunteer { get; set; }

        public TaskStatus Status { get; set; } = TaskStatus.Open;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
