namespace HumanityHub.DTOs
{
    public class CampaignUpdateDto
    {
        public required string Title { get; set; }

        public required string DescriptionIssue { get; set; }

        public decimal GoalAmount { get; set; }

        public bool IsActive { get; set; } 
    }
}
