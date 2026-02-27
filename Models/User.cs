namespace HumanityHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<Campaign> Campaigns { get; set; } = new List<Campaign>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
