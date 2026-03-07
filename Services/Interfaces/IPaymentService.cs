using HumanityHub.DTOs;

namespace HumanityHub.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<string> CreateCheckoutSessionAsync(CreateCheckoutSessionDto dto);
        Task HandleWebhookAsync(string json, string stripeSignature);
    }
}
