using HumanityHub.AppExceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
namespace HumanityHub.Middleware
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
            int statusCode;
            string message;

            _logger.LogError(exception, "An exception occurred.");

            if (exception is HumanityHubException appException)
            {
                statusCode = appException.StatusCode;
                message = appException.Message;
                context.Response.StatusCode = appException.StatusCode;
            }
            else
            {
                statusCode = StatusCodes.Status500InternalServerError;
                message = "An unexpected error occurred.";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = statusCode,
                Title = ReasonPhrases.GetReasonPhrase(statusCode),
                Detail = message,
                Instance = context.Request.Path
            }, cancellationToken);

            return true; 
        }
    }
}
