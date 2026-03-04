using HumanityHub.AppExceptions;
using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Models;
using HumanityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace HumanityHub.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ApplicationDbContext _db;
        public CampaignService(ApplicationDbContext db)
        {
            this._db = db;
        }
            public async Task<CampaignResponseDto> CreateCampaignAsync(CreateCampaignDto createDto)
            {
            var newCampaign = new Campaign
            {
                Title = createDto.Title,
                DescriptionIssue = createDto.DescriptionIssue,
                GoalAmount = createDto.GoalAmount <= 0 ? throw new BadRequestException("Goal amount must be greater than zero.") : createDto.GoalAmount,
            };
            _db.Campaigns.Add(newCampaign);
            await _db.SaveChangesAsync();
            return new CampaignResponseDto
            {
                Id = newCampaign.Id,
                Title = newCampaign.Title,
                DescriptionIssue = newCampaign.DescriptionIssue,
                GoalAmount = newCampaign.GoalAmount,
                CurrentAmount = newCampaign.CurrentAmount,
                IsActive = newCampaign.IsActive,
                Donations = new List<DonationResponseDto>()
            };
        }

        public async Task<bool> DeleteCampaignAsync(int id)
        {
            var existingCampaign = await _db.Campaigns.FindAsync(id);
            if (existingCampaign == null) return false;
            _db.Campaigns.Remove(existingCampaign);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<CampaignResponseDto>> GetAllCampaigns()
        {
            var campaigns = await _db.Campaigns.Select(c => new CampaignResponseDto
            {
                Id = c.Id,
                Title = c.Title,
                DescriptionIssue = c.DescriptionIssue,
                GoalAmount = c.GoalAmount,
                CurrentAmount = c.CurrentAmount,
                IsActive = c.IsActive,
                Donations = c.Donations.Select(d => new DonationResponseDto
                {
                    Id = d.Id,
                    CampaignId = d.CampaignId,
                    Amount = d.Amount,
                    DonorName = d.DonorName,
                    PaymentMethod = d.PaymentMethod,
                    Status = d.Status
                }).ToList()
            }).ToListAsync();
            return campaigns;
        }

        public async Task<bool> UpdateCampaignAsync(int id, CampaignUpdateDto updateDto)
            {
            var campaign = await _db.Campaigns.FindAsync(id);
            if (campaign == null) return false;
            if (!campaign.IsActive)
                throw new ConflictException("Cannot update inactive campaign.");
            campaign.Title = updateDto.Title;
            campaign.DescriptionIssue = updateDto.DescriptionIssue;
            if (campaign.CurrentAmount > campaign.GoalAmount)
                throw new BadRequestException("Current amount cannot exceed goal amount.");
            campaign.GoalAmount = updateDto.GoalAmount <= 0 ? throw new BadRequestException("Goal amount must be greater than zero.") : updateDto.GoalAmount;
            campaign.IsActive = updateDto.IsActive;
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
