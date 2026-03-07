using HumanityHub.DTOs;

namespace HumanityHub.Services.Interfaces
{
    public interface IDonationService
    {
            Task<IEnumerable<DonationResponseDto>> GetAllDonationsAsync();
            Task DeleteDonationAsync(int id);
    }
}
