using Serilog;

namespace PostmanAPI.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var startTime = DateTime.UtcNow;
        
        // Log incoming request
        _logger.LogInformation("HTTP {Method} {Path} started at {StartTime}", 
            context.Request.Method, 
            context.Request.Path, 
            startTime);

        await _next(context);

        // Log response
        var duration = DateTime.UtcNow - startTime;
        _logger.LogInformation("HTTP {Method} {Path} responded {StatusCode} in {Duration}ms", 
            context.Request.Method, 
            context.Request.Path, 
            context.Response.StatusCode,
            duration.TotalMilliseconds);
    }
}
