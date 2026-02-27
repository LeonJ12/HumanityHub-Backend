namespace HumanityHub.Models
{
    public class Campaign
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal GoalAmount { get; set; }

        public decimal CurrentAmount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
    }
}
