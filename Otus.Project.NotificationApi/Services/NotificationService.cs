using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.NotificationApi.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository<Notification, Guid> _notificationRepository;

        public NotificationService(IRepository<Notification, Guid> notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task<List<NotificationVm>> GetUserNotifications(Guid userId, CancellationToken ct)
        {
            Expression<Func<Notification, bool>> selectByUserIdSpec = notification => notification.UserId == userId;            
            var userNotifications = await _notificationRepository.FindAll()
                .Where(selectByUserIdSpec)
                .OrderBy(n => n.CreatedDate)
                .Select(n => n.ConvertToVm())
                .ToListAsync(ct);

            return userNotifications;
        }

        public async Task SendNotificationToUser(NotificationModel notificationModel, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(notificationModel.RecipientEmail) || string.IsNullOrEmpty(notificationModel.Message))
            {
                throw new InvalidOperationException("Both, recipient's email and message must not be empty!");
            }
            
            var newNotification = notificationModel.ConvertToDomainModel();
            _notificationRepository.Add(newNotification);
            await _notificationRepository.CommitChangesAsync(ct);
        }
    }
}
