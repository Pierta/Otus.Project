using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Services
{
    public class StockReservedConsumer : IConsumeAsync<StockReserved>
    {
        private readonly IDeliveryService _deliveryService;

        public StockReservedConsumer(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        public Task ConsumeAsync(StockReserved stockModel, CancellationToken cancellationToken = default)
        {
            return _deliveryService.ReserveDelivery(stockModel, cancellationToken);
        }
    }
}
