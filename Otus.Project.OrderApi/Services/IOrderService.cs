using Otus.Project.OrderApi.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public interface IOrderService
    {
        Task<OrderVm> CreateOrder(Guid userId, OrderModel orderModel, CancellationToken ct);

        Task<OrderVm> GetOrderById(Guid orderId, CancellationToken ct);

        Task<List<OrderVm>> GetUserOrders(Guid userId, CancellationToken ct);

        Task<List<ProductVm>> GetProducts(CancellationToken ct);
    }
}
