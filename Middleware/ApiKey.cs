namespace HumanityHub.Middleware
{

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute { }
    public class ApiKey
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyHeader = "X-Api-Key";
        public ApiKey(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
        {
            var endpoint = context.GetEndpoint();
            var hasApiKey = endpoint?.Metadata.GetMetadata<ApiKeyAttribute>() != null;

            if (!hasApiKey || context.Request.Path.StartsWithSegments("/api/payment/webhook"))
            {
                await _next(context);
                return;
            }

            if (!context.Request.Headers.TryGetValue(ApiKeyHeader, out var extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("API Key missing.");
                return;
            }

            var apiKey = configuration["ApiKey"];
            if (!apiKey!.Equals(extractedApiKey))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync("Invalid API Key.");
                return;
            }

            await _next(context);
        }
    }   
}
