using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Services
{
    public class PaymentRejectedConsumer : IConsumeAsync<PaymentRejected>
    {
        private readonly IDeliveryService _deliveryService;

        public PaymentRejectedConsumer(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public Task ConsumeAsync(PaymentRejected paymentModel, CancellationToken cancellationToken = default)
        {
            return _deliveryService.ReleaseDelivery(paymentModel.OrderId, paymentModel.Products, cancellationToken);
        }
    }
}
