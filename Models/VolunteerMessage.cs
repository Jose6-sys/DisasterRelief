using System.ComponentModel.DataAnnotations;


namespace DisasterRelief.Models
{
    public class VolunteerMessage
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }


        public string ReceiverId { get; set; }
        public ApplicationUser Receiver { get; set; }


        [Required]
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}