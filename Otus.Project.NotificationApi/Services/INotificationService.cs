using Otus.Project.NotificationApi.Contracts;
using Otus.Project.NotificationApi.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Services
{
    public interface INotificationService
    {
        Task<List<NotificationVm>> GetUserNotifications(Guid userId, CancellationToken ct);

        Task SendNotificationToUser(NotificationModel notificationModel, CancellationToken ct);
    }
}
