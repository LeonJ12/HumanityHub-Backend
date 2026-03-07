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
