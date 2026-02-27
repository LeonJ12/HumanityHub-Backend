namespace HumanityHub.Models
{
    public enum DonationStatus
    {
        Pending,
        Completed,
        Failed
    }
    public class Donation
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; } = null!;

        public decimal Amount { get; set; }

        public string DonorName { get; set; } = string.Empty;

        public string DonorEmail { get; set; } = string.Empty;

        public required string PaymentMethod { get; set; }

        public DonationStatus Status { get; set; } = DonationStatus.Pending;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
