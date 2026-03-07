using HumanityHub.DTOs;
using HumanityHub.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HumanityHub.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost("checkout")]
        public async Task<IActionResult> CreateCheckoutSession([FromBody] CreateCheckoutSessionDto dto)
        {
            var sessionUrl = await _paymentService.CreateCheckoutSessionAsync(dto);
            return Ok(new { url = sessionUrl });
        }
        [HttpPost("webhook")]
        public async Task<IActionResult> HandleWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            var stripeSignature = Request.Headers["Stripe-Signature"];
            await _paymentService.HandleWebhookAsync(json, stripeSignature!);
            return Ok();
        }
    }
}
