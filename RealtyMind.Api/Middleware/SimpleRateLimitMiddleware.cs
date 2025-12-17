using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace RealtyMind.Api.Middleware
{
    public class SimpleRateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        private const int LIMIT = 100; // requests
        private static readonly TimeSpan WINDOW = TimeSpan.FromMinutes(1);

        public SimpleRateLimitMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var key = $"rl_{ip}";

            var count = _cache.GetOrCreate(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = WINDOW;
                return 0;
            });

            if (count >= LIMIT)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded");
                return;
            }

            _cache.Set(key, count + 1);
            await _next(context);
        }
    }
}
