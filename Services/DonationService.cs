using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Services.Interfaces;
using HumanityHub.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            if (campaign == null) throw new Exception("Campaign not found");
            if (!campaign.IsActive) throw new Exception("Campaign is not active");
            if (campaign.CurrentAmount + createDonationDto.Amount > campaign.GoalAmount) throw new Exception("Donation exceeds campaign goal");

            var newDonation = new Donation
            {
                CampaignId = createDonationDto.CampaignId,
                Amount = createDonationDto.Amount,
                DonorName = createDonationDto.DonorName,
                DonorEmail = createDonationDto.DonorEmail,
                PaymentMethod = "Credit Card",
                Status = DonationStatus.Completed
            };

            _db.Donations.Add(newDonation);
            campaign.CurrentAmount += newDonation.Amount;
            if (campaign.CurrentAmount >= campaign.GoalAmount) campaign.IsActive = false;

            await _db.SaveChangesAsync();

            var response = new DonationResponseDto
            {
                Id = newDonation.Id,
                CampaignId = newDonation.CampaignId,
                Amount = newDonation.Amount,
                DonorName = newDonation.DonorName,
                PaymentMethod = newDonation.PaymentMethod,
                Status = newDonation.Status
            };
            return response;
        }

        public async Task<bool> DeleteDonationAsync(int id)
        {
            var existingDonation = await _db.Donations.FindAsync(id);
            if (existingDonation == null) return false;
            _db.Donations.Remove(existingDonation);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DonationResponseDto>> GetAllDonationsAsync()
        {
            var donations = await _db.Donations.
            Select(d => new DonationResponseDto
            {
                Id = d.Id,
                CampaignId = d.CampaignId,
                Amount = d.Amount,
                DonorName = d.DonorName,
                PaymentMethod = d.PaymentMethod,
                Status = d.Status
            }).
        ToListAsync();
            return donations;
        }
    }
}
