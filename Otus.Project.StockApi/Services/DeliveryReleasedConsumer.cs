using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Services
{
    public class DeliveryReleasedConsumer : IConsumeAsync<DeliveryReleased>
    {
        private readonly IStockService _stockService;

        public DeliveryReleasedConsumer(IStockService stockService)
        {
            _stockService = stockService;
        }

        public Task ConsumeAsync(DeliveryReleased deliverySlotModel, CancellationToken cancellationToken = default)
        {
            return _stockService.ReleaseStock(deliverySlotModel.OrderId, deliverySlotModel.Products, cancellationToken);
        }
    }
}
