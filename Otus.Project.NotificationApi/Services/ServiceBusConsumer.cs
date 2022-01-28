using EasyNetQ.AutoSubscribe;
using Otus.Project.NotificationApi.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Services
{
    public class ServiceBusConsumer : IConsumeAsync<NotificationModel>
    {
        public const string SubscriptionIdPrefix = "OrderNotification";

        private readonly INotificationService _notificationService;

        public ServiceBusConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task ConsumeAsync(NotificationModel message, CancellationToken cancellationToken = default)
        {
            return _notificationService.SendNotificationToUser(message, cancellationToken);
        }
    }
}
