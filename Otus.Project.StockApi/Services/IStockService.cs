using Otus.Project.MessageBus.Contracts;
using Otus.Project.StockApi.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Services
{
    public interface IStockService
    {
        Task ReserveStock(OrderCreated orderModel, CancellationToken ct = default);

        Task ReleaseStock(Guid orderId, List<Guid> products, CancellationToken ct = default);

        Task<StockVm> GetStockByProductId(Guid productId, CancellationToken ct);

        Task<List<StockVm>> GetStocks(CancellationToken ct);
    }
}
