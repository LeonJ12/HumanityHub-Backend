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
            try
            {
                var campaigns = await _campaignService.GetAllCampaigns();
                return Ok(campaigns);

            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred  fetching campaigns.");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateCampaign([FromBody] CreateCampaignDto createCampaignDto)
        {
            try
            {
                var newCampaign = await _campaignService.CreateCampaignAsync(createCampaignDto);
                return Ok(newCampaign);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the campaign.");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCampaign(int id, [FromBody] CampaignUpdateDto campaignUpdateDto)
        {
            try
            {
                var campaign = await _campaignService.UpdateCampaignAsync(id, campaignUpdateDto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the campaign.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCampaign(int id)
        {
            try
            {
                var campaign = await _campaignService.DeleteCampaignAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the campaign.");
            }
        }
    }
}
