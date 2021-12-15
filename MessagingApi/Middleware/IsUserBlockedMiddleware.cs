using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace MessagingApi.Middleware
{
    public class IsUserBlockedMiddleware
    {
        RequestDelegate _next;

        public IsUserBlockedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService service)
        {
            User user = await service.GetCurrentUserFromHttp(context);

            if (user != null && user.Blocked)
            {
                context.Response.Clear();
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                await context.Response.WriteAsync("You are currently blocked, contact admin for more information.");
            }

            else
            {
                await _next.Invoke(context);
            }
        }   
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseUserBlockedMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<IsUserBlockedMiddleware>();
        }
    }
}
