using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class PaymentCompletedConsumer : IConsumeAsync<PaymentCompleted>
    {
        private readonly ISagaOrderService _sagaOrderService;

        public PaymentCompletedConsumer(ISagaOrderService sagaOrderService)
        {
            _sagaOrderService = sagaOrderService;
        }

        public Task ConsumeAsync(PaymentCompleted paymentModel, CancellationToken cancellationToken = default)
        {
            return _sagaOrderService.CompleteOrder(paymentModel.OrderId, cancellationToken);
        }
    }
}
