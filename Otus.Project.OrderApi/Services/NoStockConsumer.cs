using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class NoStockConsumer : IConsumeAsync<NoStock>
    {
        private readonly ISagaOrderService _sagaOrderService;

        public NoStockConsumer(ISagaOrderService sagaOrderService)
        {
            _sagaOrderService = sagaOrderService;
        }

        public Task ConsumeAsync(NoStock stockModel, CancellationToken cancellationToken = default)
        {
            return _sagaOrderService.RejectOrder(stockModel.OrderId, cancellationToken);
        }
    }
}
