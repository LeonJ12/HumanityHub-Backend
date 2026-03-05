using System.ComponentModel.DataAnnotations;

namespace HumanityHub.DTOs
{
    public class CampaignUpdateDto
    {
        public required string Title { get; set; }

        public required string DescriptionIssue { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = "Goal amount must be greater than zero.")]
        public decimal GoalAmount { get; set; }

        public bool IsActive { get; set; } 
    }
}
