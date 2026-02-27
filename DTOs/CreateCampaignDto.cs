namespace HumanityHub.DTOs
{
    public class CreateCampaignDto
    {
        public required string Title { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal GoalAmount { get; set; }
    }
}
