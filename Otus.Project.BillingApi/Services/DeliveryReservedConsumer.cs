using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.BillingApi.Services
{
    public class DeliveryReservedConsumer : IConsumeAsync<DeliveryReserved>
    {
        private readonly IBillingAccountService _billingAccountService;

        public DeliveryReservedConsumer(IBillingAccountService billingAccountService)
        {
            _billingAccountService = billingAccountService;
        }

        public Task ConsumeAsync(DeliveryReserved deliveryModel, CancellationToken cancellationToken = default)
        {
            return _billingAccountService.PayForTheOrder(deliveryModel, cancellationToken);
        }
    }
}
