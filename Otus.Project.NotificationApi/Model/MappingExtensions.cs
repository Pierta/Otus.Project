using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.NotificationApi.Model
{
    public static class MappingExtensions
    {
        public static NotificationVm ConvertToVm(this Notification notification)
        {
            return new NotificationVm
            {
                Id = notification.Id,
                CreatedDate = notification.CreatedDate,
                UpdatedDate = notification.UpdatedDate,
                UserId = notification.UserId,
                RecipientEmail = notification.RecipientEmail,
                OrderId = notification.OrderId,
                OrderCost = notification.Order.Cost,
                OrderIsPaid = notification.Order.IsPaid,
                Message = notification.Message
            };
        }

        public static Notification ConvertToDomainModel(this NotificationModel notificationModel)
        {
            var now = DateTime.UtcNow;
            return new Notification
            {
                CreatedDate = now,
                UpdatedDate = now,
                UserId = notificationModel.UserId,
                OrderId = notificationModel.OrderId,
                RecipientEmail = notificationModel.RecipientEmail,
                Message = notificationModel.Message
            };
        }
    }
}
