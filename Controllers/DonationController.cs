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
    public class DonationController : ControllerBase
    {
        private readonly IDonationService _service;
        public DonationController(IDonationService service)
        {
            this._service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetDonations()
        {
                var donations = await _service.GetAllDonationsAsync();
                return Ok(donations);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDonations(int id)
        {
                var result = await _service.DeleteDonationAsync(id);
                return NoContent();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDonation([FromBody] CreateDonationDto createDonationDto)
        {
                var response = await _service.CreateDonationAsync(createDonationDto);
                return Ok(response);
        }
    }
}
