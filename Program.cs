
using HumanityHub.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using HumanityHub.Services;

namespace HumanityHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<Data.ApplicationDbContext>(options =>
                options.UseSqlite(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                    ?? "Data Source=humanityhub.db"));
            builder.Services.AddScoped<ICampaignService, CampaignService>();
            builder.Services.AddScoped<IDonationService, DonationService>();

            builder.Services.AddControllers();
            
            builder.Services.AddOpenApi();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend",
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod();
                    });
            });
            var app = builder.Build();

            app.UseCors("AllowFrontend");
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/openapi/v1.json", "HumanityHub API v1")
                );
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
