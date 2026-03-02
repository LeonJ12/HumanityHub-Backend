using HumanityHub.AppExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HumanityHub.AppExceptions
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext context,
            Exception exception,
            CancellationToken cancellationToken)
        {
            
            _logger.LogError(exception, "An exception occurred.");

            ProblemDetails problemDetails;

            
            if (exception is HumanityHubException appException)
            {
                problemDetails = new ProblemDetails
                {
                    Status = appException.StatusCode,
                    Title = GetTitle(appException.StatusCode),
                    Detail = appException.Message,
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = appException.StatusCode;
            }
            else
            {
                
                problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Server Error",
                    Detail = "An unexpected error occurred.",
                    Instance = context.Request.Path
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true; 
        }

        private static string GetTitle(int statusCode)
        {
            return statusCode switch
            {
                400 => "Bad Request",
                404 => "Not Found",
                409 => "Conflict",
                _ => "Error"
            };
        }
    }
}
