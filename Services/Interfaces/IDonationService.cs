using HumanityHub.DTOs;

namespace HumanityHub.Services.Interfaces
{
    public interface IDonationService
    {
            Task<IEnumerable<DonationResponseDto>> GetAllDonationsAsync();
            Task<DonationResponseDto> CreateDonationAsync(CreateDonationDto createDonationDto);
            Task<bool> DeleteDonationAsync(int id);
    }
}
