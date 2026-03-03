using System.ComponentModel.DataAnnotations;

namespace HumanityHub.DTOs
{
    public class CreateDonationDto
    {
        [Required]
        public int CampaignId { get; set; }

        [Range(1,1000)]
        public decimal Amount { get; set; }
        [Required]
        [MinLength(3)]
        public string DonorName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string DonorEmail { get; set; } = string.Empty;
    }
}
