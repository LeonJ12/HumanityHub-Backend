using HumanityHub.DTOs;
using HumanityHub.Models;

namespace HumanityHub.Extensions
{
    public static class MappingExtensions
    {
        public static CampaignResponseDto ToCampaignResponseDto(this Campaign campaign)
        {
            return new CampaignResponseDto
            {
                Id = campaign.Id,
                Title = campaign.Title,
                DescriptionIssue = campaign.DescriptionIssue,
                GoalAmount = campaign.GoalAmount,
                CurrentAmount = campaign.CurrentAmount,
                IsActive = campaign.IsActive,
                Donations = campaign.Donations.Select(d => d.ToDonationResponseDto()).ToList()
            };
        }
        public static DonationResponseDto ToDonationResponseDto(this Donation donation)
        {
            return new DonationResponseDto
            {
                Id = donation.Id,
                CampaignId = donation.CampaignId,
                Amount = donation.Amount,
                DonorName = donation.DonorName,
                PaymentMethod = donation.PaymentMethod,
                Status = donation.Status
            };
        }
    }
}
