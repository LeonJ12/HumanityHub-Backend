using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Services.Interfaces;
using HumanityHub.Models;
using Microsoft.EntityFrameworkCore;
using HumanityHub.AppExceptions;
using HumanityHub.Extensions;
namespace HumanityHub.Services
{
    public class DonationService : IDonationService
    {
        private readonly ApplicationDbContext _db;
        public DonationService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<DonationResponseDto> CreateDonationAsync(CreateDonationDto createDonationDto)
        {
            var campaign = await _db.Campaigns.FindAsync(createDonationDto.CampaignId);
            if (campaign == null) throw new NotFoundException("Campaign not found");
            if (!campaign.IsActive) throw new ConflictException("Campaign is not active");
            if (campaign.CurrentAmount + createDonationDto.Amount > campaign.GoalAmount) throw new BadRequestException("Donation exceeds campaign goal");

            var newDonation = new Donation
            {
                CampaignId = createDonationDto.CampaignId,
                Amount = createDonationDto.Amount,
                DonorName = createDonationDto.DonorName,
                DonorEmail = createDonationDto.DonorEmail,
                PaymentMethod = "Credit Card", //payment logic soon
                Status = DonationStatus.Completed
            };

            _db.Donations.Add(newDonation);
            campaign.CurrentAmount += newDonation.Amount;
            if (campaign.CurrentAmount >= campaign.GoalAmount) campaign.IsActive = false;

            await _db.SaveChangesAsync();
            return newDonation.ToDonationResponseDto();
        }

        public async Task DeleteDonationAsync(int id)
        {
            var existingDonation = await _db.Donations.FindAsync(id);
            if (existingDonation == null) throw new NotFoundException($"Donation with id {id} not found.");
            var campaign = await _db.Campaigns.FindAsync(existingDonation.CampaignId);
            if(campaign == null) throw new NotFoundException("Associated campaign not found");
            campaign.CurrentAmount -= existingDonation.Amount;
            if(campaign.CurrentAmount < campaign.GoalAmount) campaign.IsActive = true;
            _db.Donations.Remove(existingDonation);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<DonationResponseDto>> GetAllDonationsAsync()
        {
            var donations = await _db.Donations.
            Select(d => d.ToDonationResponseDto()).ToListAsync();
            return donations;
        }
    }
}
