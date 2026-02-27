namespace HumanityHub.DTOs
{
    public class CampaignUpdateDto
    {
        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal GoalAmount { get; set; }

        public bool IsActive { get; set; } 
    }
}
