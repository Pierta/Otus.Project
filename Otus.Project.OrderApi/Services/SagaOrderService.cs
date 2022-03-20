using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.OrderApi.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class SagaOrderService : ISagaOrderService
    {
        private const string EmailNotFound = "email-not-found@example.com";

        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IBus _bus;

        public SagaOrderService(IRepository<Order, Guid> orderRepository,
            IRepository<Product, Guid> productRepository,
            IRepository<User, Guid> userRepository,
            IBus bus)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _bus = bus;
        }

        public async Task<OrderVm> CreateOrder(Guid userId, OrderModel orderModel, CancellationToken ct = default)
        {
            Expression<Func<Product, bool>> selectByProductIdsSpec = product => orderModel.Products.Contains(product.Id);
            var totalCost = _productRepository.FindAll()
                .Where(selectByProductIdsSpec)
                .Sum(p => p.Cost);

            var now = DateTime.UtcNow;
            var newOrder = new Order
            {
                CreatedDate = now,
                UpdatedDate = now,
                UserId = userId,
                Cost = totalCost,
                IsPaid = false,
                OrderState = OrderState.Pending,
                Products = orderModel.Products.Select(productId => new OrderProducts
                {
                    CreatedDate = now,
                    UpdatedDate = now,
                    ProductId = productId
                }).ToList()
            };
            _orderRepository.Add(newOrder);
            await _orderRepository.CommitChangesAsync(ct);

            var currenUser = await _userRepository.FindByID(userId, ct);

            await _bus.PubSub.PublishAsync(new OrderCreated
            {
                UserId = userId,
                OrderId = newOrder.Id,
                Cost = newOrder.Cost,
                Products = orderModel.Products,
                RecipientEmail = currenUser?.Email ?? EmailNotFound,
                Message = $"Order with id = {newOrder.Id} has been created"
            }, ct);

            Expression<Func<Order, bool>> selectByOrderIdSpec = order => order.Id == newOrder.Id;
            var existingOrder = await _orderRepository.FindAll()
                .Where(selectByOrderIdSpec)
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product)
                .SingleOrDefaultAsync(ct);

            return existingOrder.ConvertToVm();
        }

        public async Task CompleteOrder(Guid orderId, CancellationToken ct = default)
        {
            var existingOrder = await _orderRepository.FindByID(orderId, ct);

            existingOrder.UpdatedDate = DateTime.UtcNow;
            existingOrder.IsPaid = true;
            existingOrder.OrderState = OrderState.Completed;

            await _orderRepository.CommitChangesAsync(ct);
        }

        public async Task RejectOrder(Guid orderId, CancellationToken ct = default)
        {
            var existingOrder = await _orderRepository.FindByID(orderId, ct);

            existingOrder.UpdatedDate = DateTime.UtcNow;
            existingOrder.OrderState = OrderState.Rejected;

            await _orderRepository.CommitChangesAsync(ct);
        }
    }
}
