using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.Orm.Repository;
using Otus.Project.StockApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.StockApi.Services
{
    public class StockService : IStockService
    {
        private readonly IRepository<Stock, Guid> _stockRepository;
        private readonly IBus _bus;

        public StockService(IRepository<Stock, Guid> stockRepository,
            IBus bus)
        {
            _stockRepository = stockRepository;
            _bus = bus;
        }

        public async Task ReserveStock(OrderCreated orderModel, CancellationToken ct = default)
        {         
            Expression<Func<Stock, bool>> selectByProductIdsSpec = stock => orderModel.Products.Contains(stock.ProductId);
            var stocks = await _stockRepository.FindAll()
                .Where(selectByProductIdsSpec)
                .ToListAsync(ct);

            var now = DateTime.UtcNow;
            foreach (var stock in stocks)
            {
                stock.UpdatedDate = now;
                if (stock.Amount > 0)
                    stock.Amount--;
                else
                {
                    await _bus.PubSub.PublishAsync(new NoStock
                    {
                        OrderId = orderModel.OrderId
                    }, ct);
                    return;
                }
            }

            await _stockRepository.CommitChangesAsync(ct);

            await _bus.PubSub.PublishAsync(new StockReserved
            {
                UserId = orderModel.UserId,
                OrderId = orderModel.OrderId,
                Products = orderModel.Products,
                Cost = orderModel.Cost
            }, ct);
        }

        public async Task ReleaseStock(Guid orderId, List<Guid> products, CancellationToken ct = default)
        {
            Expression<Func<Stock, bool>> selectByProductIdsSpec = stock => products.Contains(stock.ProductId);
            var stocks = await _stockRepository.FindAll()
                .Where(selectByProductIdsSpec)
                .ToListAsync(ct);

            var now = DateTime.UtcNow;
            foreach (var stock in stocks)
            {
                stock.UpdatedDate = now;
                stock.Amount++;
            }

            await _stockRepository.CommitChangesAsync(ct);

            await _bus.PubSub.PublishAsync(new StockReleased
            {
                OrderId = orderId
            }, ct);
        }

        public async Task<StockVm> GetStockByProductId(Guid productId, CancellationToken ct)
        {
            Expression<Func<Stock, bool>> selectByProductIdSpec = stock => stock.ProductId == productId;
            var existingStock = await _stockRepository.FindAll()
                .Where(selectByProductIdSpec)
                .Include(s => s.Product)
                .SingleOrDefaultAsync(ct);
            if (existingStock == null)
            {
                throw new KeyNotFoundException($"Stocks for product with Id = '{productId}' are not found in a database!");
            }

            return existingStock.ConvertToVm();
        }

        public async Task<List<StockVm>> GetStocks(CancellationToken ct)
        {
            var stocks = await _stockRepository.FindAll()
                .Include(s => s.Product)
                .OrderBy(n => n.Amount)
                .Select(n => n.ConvertToVm())
                .ToListAsync(ct);

            return stocks;
        }
    }
}
