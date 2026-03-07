
using HumanityHub.DTOs;
using HumanityHub.Middleware;
using HumanityHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        [ApiKey]
        public async Task<IActionResult> GetDonations()
        {
                var donations = await _service.GetAllDonationsAsync();
                return Ok(donations);
        }
        [HttpDelete("{id}")]
        [ApiKey]
        public async Task<IActionResult> DeleteDonations(int id)
        {
                await _service.DeleteDonationAsync(id);
                return NoContent();
        }
    }
}
