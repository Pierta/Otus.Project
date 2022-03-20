using EasyNetQ.AutoSubscribe;
using Otus.Project.MessageBus.Contracts;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Services
{
    public class NoFreeSlotsConsumer : IConsumeAsync<NoFreeSlots>
    {
        private readonly IStockService _stockService;

        public NoFreeSlotsConsumer(IStockService stockService)
        {
            _stockService = stockService;
        }

        public Task ConsumeAsync(NoFreeSlots deliverySlotModel, CancellationToken cancellationToken = default)
        {
            return _stockService.ReleaseStock(deliverySlotModel.OrderId, deliverySlotModel.Products, cancellationToken);
        }
    }
}
