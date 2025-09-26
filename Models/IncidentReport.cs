using System;
using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class IncidentReport
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Location { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Date Occurred")]
        public DateTime DateOccurred { get; set; } = DateTime.UtcNow;

        [DataType(DataType.Date)]
        [Display(Name = "Date Reported")]
        public DateTime DateReported { get; set; } = DateTime.UtcNow;  // <-- Added this

        public SeverityLevel Severity { get; set; } = SeverityLevel.Medium;

        public IncidentStatus Status { get; set; } = IncidentStatus.Open;

        // Reporter
        public string ReporterId { get; set; }
        public ApplicationUser Reporter { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
