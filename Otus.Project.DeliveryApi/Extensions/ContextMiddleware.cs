using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Extensions
{
    public class ContextMiddleware
    {
        private readonly RequestDelegate _next;

        public ContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var user = context.User;
            string userIdStr;
            if (user != null && (userIdStr = user.FindFirst("id")?.Value) != null && Guid.TryParse(userIdStr, out Guid userId))
            {
                context.Items["UserId"] = userId;
            };

            await _next(context);
        }
    }
}
