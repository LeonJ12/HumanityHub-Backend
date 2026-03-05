using HumanityHub.DTOs;

namespace HumanityHub.Services.Interfaces
{
    public interface ICampaignService
    {
        Task<CampaignResponseDto> CreateCampaignAsync(CreateCampaignDto createDto);
        Task<IEnumerable<CampaignResponseDto>> GetAllCampaigns();
        Task UpdateCampaignAsync(int id, CampaignUpdateDto updateDto);

        Task DeleteCampaignAsync(int id);
    }
}
