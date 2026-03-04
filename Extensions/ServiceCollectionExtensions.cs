using HumanityHub.Services;
using HumanityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HumanityHub.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlite(
                    configuration.GetConnectionString("DefaultConnection")
                    ?? "Data Source=humanityhub.db"));
            return services;
        }
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICampaignService, CampaignService>();
            services.AddScoped<IDonationService, DonationService>();
            return services;
        }
    }
}
