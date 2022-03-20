using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Services
{
    public class OrderCreatedConsumer : IConsumeAsync<OrderCreated>
    {
        private readonly IStockService _stockService;

        public OrderCreatedConsumer(IStockService stockService)
        {
            _stockService = stockService;
        }

        public Task ConsumeAsync(OrderCreated orderModel, CancellationToken cancellationToken = default)
        {
            return _stockService.ReserveStock(orderModel, cancellationToken);
        }
    }
}
