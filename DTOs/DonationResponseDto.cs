using HumanityHub.Models;

namespace HumanityHub.DTOs
{
    public class DonationResponseDto
    {
        public int Id { get; set; }

        public int CampaignId { get; set; }
        public decimal Amount { get; set; }

        public required string DonorName { get; set; } 

        public required string PaymentMethod { get; set; }

        public DonationStatus Status { get; set; }
    }
}
