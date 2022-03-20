using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class StockReleasedConsumer : IConsumeAsync<StockReleased>
    {
        private readonly ISagaOrderService _sagaOrderService;

        public StockReleasedConsumer(ISagaOrderService sagaOrderService)
        {
            _sagaOrderService = sagaOrderService;
        }

        public Task ConsumeAsync(StockReleased stockModel, CancellationToken cancellationToken = default)
        {
            return _sagaOrderService.RejectOrder(stockModel.OrderId, cancellationToken);
        }
    }
}
