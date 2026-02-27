namespace HumanityHub.DTOs
{
    public class CreateDonationDto
    {
        public int CampaignId { get; set; }

        public decimal Amount { get; set; }

        public string DonorName { get; set; } = string.Empty;

        public string DonorEmail { get; set; } = string.Empty;
    }
}
