using HumanityHub.Services;
using HumanityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Stripe;

namespace HumanityHub.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                    ?? throw new InvalidOperationException("Connection string not found.")));
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IPaymentService, StripePaymentService>();
            StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];
            return services;
        }
    }
}
