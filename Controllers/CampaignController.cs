using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Models;
using HumanityHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HumanityHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ICampaignService _campaignService;
        public CampaignController(ICampaignService service)
        {
            this._campaignService = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetCampaigns()
        {
                var campaigns = await _campaignService.GetAllCampaigns();
                return Ok(campaigns);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
                var newCampaign = await _campaignService.CreateCampaignAsync(createCampaignDto);
                return Ok(newCampaign);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignUpdateDto campaignUpdateDto)
        {
                var campaign = await _campaignService.UpdateCampaignAsync(id, campaignUpdateDto);
                return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
                var campaign = await _campaignService.DeleteCampaignAsync(id);
                return NoContent();
        }
    }
}
