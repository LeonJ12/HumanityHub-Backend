using HumanityHub.Extensions;
using HumanityHub.Middleware;

namespace HumanityHub
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddDatabase(builder.Configuration)
                .AddApplicationServices()
                .AddOpenApi()
                .AddCors(options =>
                {
                    options.AddPolicy("AllowFrontend", policy =>
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());
                })
                .AddExceptionHandler<GlobalExceptionHandler>()
                .AddProblemDetails()
                .AddControllers();

            var app = builder.Build();

            app.UseCors("AllowFrontend");

            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.UseSwaggerUI(options =>
                options.SwaggerEndpoint("/openapi/v1.json", "HumanityHub API v1")
                );
            }
            app.UseExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
