namespace HumanityHub.DTOs
{
    public class CreateCampaignDto
    {
        public required string Title { get; set; }

        public required string DescriptionIssue { get; set; }

        public decimal GoalAmount { get; set; }
    }
}
