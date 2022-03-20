using Otus.Project.OrderApi.Model;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public interface ISagaOrderService
    {
        Task<OrderVm> CreateOrder(Guid userId, OrderModel orderModel, CancellationToken ct);

        Task CompleteOrder(Guid orderId, CancellationToken ct = default);

        Task RejectOrder(Guid orderId, CancellationToken ct = default);
    }
}
