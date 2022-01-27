using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Otus.Project.NotificationApi.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class NotificationsController : ControllerBase
    {
        private readonly ILogger<NotificationsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly INotificationService _notificationService;

        public NotificationsController(ILogger<NotificationsController> logger,
            IHttpContextAccessor httpContextAccessor,
            INotificationService notificationService)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
        {
            _logger.LogInformation("'Get a list of notifications' action has been requested");

            var userIdFromToken = (Guid?)_httpContextAccessor.HttpContext.Items["UserId"];
            var userNotifications = await _notificationService.GetUserNotifications(userIdFromToken.Value, ct);
            return Ok(userNotifications);
        }
    }
}
