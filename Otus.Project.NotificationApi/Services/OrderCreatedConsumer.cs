using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.NotificationApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Services
{
    public class OrderCreatedConsumer : IConsumeAsync<OrderCreated>
    {
        private readonly INotificationService _notificationService;

        public OrderCreatedConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task ConsumeAsync(OrderCreated message, CancellationToken cancellationToken = default)
        {
            var notificationModel = new NotificationModel
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                EventType = nameof(OrderCreated),
                RecipientEmail = message.RecipientEmail,
                Message = message.Message
            };
            return _notificationService.SendNotificationToUser(notificationModel, cancellationToken);
        }
    }
}
