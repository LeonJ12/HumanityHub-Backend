using System.ComponentModel.DataAnnotations;

namespace HumanityHub.DTOs
{
    public class CreateCheckoutSessionDto
    {
        [Required]
        public int CampaignId { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than zero.")]
        public decimal Amount { get; set; }
        [Required]
        [RegularExpression(@"^[A-Za-zČĆŽŠĐčćžšđ]+( [A-Za-zČĆŽŠĐčćžšđ]+)+$", ErrorMessage = "Unesite ime i prezime.")]
        public string DonorName { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string DonorEmail { get; set; } = string.Empty;
        public string SuccessUrl { get; set; } = string.Empty;
        public string CancelUrl { get; set; } = string.Empty;
    }
}
