using HumanityHub.Models;
using System.Collections;

namespace HumanityHub.DTOs
{
    public class CampaignResponseDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string DescriptionIssue { get; set; }

        public decimal GoalAmount { get; set; }

        public decimal CurrentAmount { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public ICollection<DonationResponseDto> Donations { get; set; } = new List<DonationResponseDto>();
    }
}
