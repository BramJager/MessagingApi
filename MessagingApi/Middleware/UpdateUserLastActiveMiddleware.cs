using MessagingApi.Business.Interfaces;
using MessagingApi.Domain.Objects;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace MessagingApi.Middleware
{
    public class UpdateUserLastActiveMiddleware
    {
        RequestDelegate _next;

        public UpdateUserLastActiveMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService service)
        {
            User user = await service.GetCurrentUserFromHttp(context);

            if (user != null && !user.Blocked)
            {
                user.LastActive = DateTime.Now;
                await service.UpdateUser(user, null);
            }

            await _next.Invoke(context);
        }
    }
}
