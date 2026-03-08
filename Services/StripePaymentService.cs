using HumanityHub.AppExceptions;
using HumanityHub.Data;
using HumanityHub.DTOs;
using HumanityHub.Models;
using HumanityHub.Services.Interfaces;
using Stripe;
using Stripe.Checkout;

namespace HumanityHub.Services
{
    public class StripePaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IConfiguration _configuration;

        public StripePaymentService(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<string> CreateCheckoutSessionAsync(CreateCheckoutSessionDto dto)
        {
            var campaign = await _db.Campaigns.FindAsync(dto.CampaignId);
            if (campaign == null) throw new NotFoundException("Campaign not found.");
            if (!campaign.IsActive) throw new ConflictException("Campaign is not active.");
            if (campaign.CurrentAmount + dto.Amount > campaign.GoalAmount)
                throw new BadRequestException("Donation exceeds campaign goal.");

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            Currency = "eur",
                            UnitAmount = (long)(dto.Amount * 100), 
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = campaign.Title,
                                Description = campaign.DescriptionIssue
                            }
                        },
                        Quantity = 1
                    }
                },
                Mode = "payment",
                CustomerEmail = dto.DonorEmail,
                SuccessUrl = dto.SuccessUrl,
                CancelUrl = dto.CancelUrl,
                Metadata = new Dictionary<string, string>
                {
                    { "campaignId", dto.CampaignId.ToString() },
                    { "amount", dto.Amount.ToString() },
                    { "donorName", dto.DonorName },
                    { "donorEmail", dto.DonorEmail }
                }
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Url;
        }

        public async Task HandleWebhookAsync(string json, string stripeSignature)
        {
            var webhookSecret = _configuration["Stripe:WebhookSecret"];

            var stripeEvent = EventUtility.ConstructEvent(
                json,
                stripeSignature,
                webhookSecret
            );
            if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                if (session == null) return;

                var campaignId = int.Parse(session.Metadata["campaignId"]);
                var amount = decimal.Parse(session.Metadata["amount"]);
                var donorName = session.Metadata["donorName"];
                var donorEmail = session.Metadata["donorEmail"];

                var campaign = await _db.Campaigns.FindAsync(campaignId);
                if (campaign == null) return;

                var donation = new Donation
                {
                    CampaignId = campaignId,
                    Amount = amount,
                    DonorName = donorName,
                    DonorEmail = donorEmail,
                    PaymentMethod = "Credit Card",
                    Status = DonationStatus.Completed
                };

                _db.Donations.Add(donation);
                campaign.CurrentAmount += amount;
                if (campaign.CurrentAmount >= campaign.GoalAmount)
                    campaign.IsActive = false;

                await _db.SaveChangesAsync();
            }
        }
    }
}
