using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BeerApi
{
    public class LoggingMiddleware 
    {
        private readonly RequestDelegate _next;
        private readonly  ILogger<LoggingMiddleware> _logger;
        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext) 
        {
            // Código que inspecciona request
            _logger.LogInformation("Request entra " + httpContext.Request.Path);

            await _next(httpContext);
            // Código que inspecciona la response
            _logger.LogInformation("Response sale " + httpContext.Response.StatusCode);
        }
    }

    public static class LoggingMiddlewareExtensions {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder app) {
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}