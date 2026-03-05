using HumanityHub.AppExceptions;
using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Models;
using HumanityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HumanityHub.Extensions;
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
                GoalAmount = createDto.GoalAmount,
            };
            _db.Campaigns.Add(newCampaign);
            await _db.SaveChangesAsync();
            return newCampaign.ToCampaignResponseDto();
        }

        public async Task DeleteCampaignAsync(int id)
        {
            var existingCampaign = await _db.Campaigns.FindAsync(id);
            if (existingCampaign == null) throw new NotFoundException($"Campaign with {id} not found.");
            _db.Campaigns.Remove(existingCampaign);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<CampaignResponseDto>> GetAllCampaigns()
        {
            var campaigns = await _db.Campaigns.Select(c => c.ToCampaignResponseDto()
            ).ToListAsync();
            return campaigns;
        }

        public async Task UpdateCampaignAsync(int id, CampaignUpdateDto updateDto)
            {
            var campaign = await _db.Campaigns.FindAsync(id);
            if (campaign == null) throw new NotFoundException($"Campaign with {id} not found.");
            if (!campaign.IsActive) throw new ConflictException("Cannot update inactive campaign.");
            if (campaign.CurrentAmount > updateDto.GoalAmount) throw new BadRequestException("Current amount cannot exceed goal amount.");
            campaign.Title = updateDto.Title;
            campaign.DescriptionIssue = updateDto.DescriptionIssue;
            campaign.GoalAmount = updateDto.GoalAmount;
            campaign.IsActive = updateDto.IsActive;
            await _db.SaveChangesAsync();
        }
    }
}
