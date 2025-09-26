using System.ComponentModel.DataAnnotations;

namespace DisasterRelief.Models
{
    public class Donation
    {
        public int Id { get; set; }

        [Required]
        public DonationType DonationType { get; set; }

        [Required]
        public string Description { get; set; }

        public int Quantity { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }

        public string? DonorId { get; set; }
        public ApplicationUser? Donor { get; set; }

        public DonationStatus Status { get; set; } = DonationStatus.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
