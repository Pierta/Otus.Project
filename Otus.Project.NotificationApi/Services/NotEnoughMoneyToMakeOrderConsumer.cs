using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.NotificationApi.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.NotificationApi.Services
{
    public class NotEnoughMoneyToMakeOrderConsumer : IConsumeAsync<NotEnoughMoneyToMakeOrder>
    {
        private readonly INotificationService _notificationService;

        public NotEnoughMoneyToMakeOrderConsumer(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public Task ConsumeAsync(NotEnoughMoneyToMakeOrder message, CancellationToken cancellationToken = default)
        {
            var notificationModel = new NotificationModel
            {
                UserId = message.UserId,
                OrderId = message.OrderId,
                EventType = nameof(NotEnoughMoneyToMakeOrder),
                RecipientEmail = message.RecipientEmail,
                Message = message.Message
            };
            return _notificationService.SendNotificationToUser(notificationModel, cancellationToken);
        }
    }
}
