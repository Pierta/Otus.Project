using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.NotificationApi.Contracts;
using Otus.Project.OrderApi.Model;
using Otus.Project.Orm.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.OrderApi.Services
{
    public class OrderService : IOrderService
    {
        private const string EmailNotFound = "email-not-found@example.com";

        private readonly IRepository<Order, Guid> _orderRepository;
        private readonly IRepository<Product, Guid> _productRepository;
        private readonly IRepository<User, Guid> _userRepository;
        private readonly IBus _bus;
        private readonly IBillingApiClient _billingApiClient;

        public OrderService(IRepository<Order, Guid> orderRepository,
            IRepository<Product, Guid> productRepository,
            IRepository<User, Guid> userRepository,
            IBus bus,
            IBillingApiClient billingApiClient)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _bus = bus;
            _billingApiClient = billingApiClient;
        }

        public async Task<OrderVm> CreateOrder(Guid userId, OrderModel orderModel, CancellationToken ct)
        {
            Expression<Func<Product, bool>> selectByProductIdsSpec = product => orderModel.Products.Contains(product.Id);
            var products = await _productRepository.FindAllByExpression(selectByProductIdsSpec, ct);
            var orderCost = products.Sum(p => p.Cost);

            // Then check if the user has enough money on his billing account
            var isMoneyEnough = await _billingApiClient.WithdrawMoney(userId, orderCost, ct);

            // Add the newly created and paid order
            var now = DateTime.UtcNow;
            var newOrder = new Order
            {
                CreatedDate = now,
                UpdatedDate = now,
                UserId = userId,
                Cost = orderCost,
                IsPaid = isMoneyEnough,
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

            if (isMoneyEnough)
            {
                await _bus.PubSub.PublishAsync(new OrderCreated
                {
                    UserId = userId,
                    OrderId = newOrder.Id,
                    RecipientEmail = currenUser?.Email ?? EmailNotFound,
                    Message = $"Happy letter! Order with id = {newOrder.Id} has been created. Congrats, you've spent ${newOrder.Cost}!"
                }, ct);
            }
            else
            {
                await _bus.PubSub.PublishAsync(new NotEnoughMoneyToMakeOrder
                {
                    UserId = userId,
                    OrderId = newOrder.Id,
                    RecipientEmail = currenUser?.Email ?? EmailNotFound,
                    Message = $"Sad letter! Order with id = {newOrder.Id} has been created, but it's not paid yet (it requires ${newOrder.Cost})."
                }, ct);
            }

            return newOrder.ConvertToVm();
        }

        public async Task<OrderVm> GetOrderById(Guid orderId, CancellationToken ct)
        {
            Expression<Func<Order, bool>> selectByOrderIdSpec = order => order.Id == orderId;
            var existingOrder = await _orderRepository.FindAll()
                .Where(selectByOrderIdSpec)
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product)
                .SingleOrDefaultAsync(ct);
            if (existingOrder == null)
            {
                throw new KeyNotFoundException($"Order with Id = '{orderId}' is not found in a database!");
            }

            return existingOrder.ConvertToVm();
        }

        public async Task<List<OrderVm>> GetUserOrders(Guid userId, CancellationToken ct)
        {
            Expression<Func<Order, bool>> selectByUserIdSpec = order => order.UserId == userId;
            var userOrders = await _orderRepository.FindAll()
                .Where(selectByUserIdSpec)
                .Include(o => o.Products)
                    .ThenInclude(op => op.Product)
                .OrderByDescending(n => n.CreatedDate)
                .Select(n => n.ConvertToVm())
                .ToListAsync(ct);

            return userOrders;
        }

        public async Task<List<ProductVm>> GetProducts(CancellationToken ct)
        {
            var products = await _productRepository.FindAll()
                .OrderBy(n => n.Name)
                .Select(n => n.ConvertToVm())
                .ToListAsync(ct);

            return products;
        }
    }
}
