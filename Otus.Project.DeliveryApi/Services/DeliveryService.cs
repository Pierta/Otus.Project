using EasyNetQ;
using Microsoft.EntityFrameworkCore;
using Otus.Project.Domain.Model;
using Otus.Project.MessageBus.Contracts;
using Otus.Project.Orm.Repository;
using Otus.Project.DeliveryApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IRepository<DeliverySlot, Guid> _deliveryRepository;
        private readonly IBus _bus;

        public DeliveryService(IRepository<DeliverySlot, Guid> deliveryRepository,
            IBus bus)
        {
            _deliveryRepository = deliveryRepository;
            _bus = bus;
        }

        public async Task ReserveDelivery(StockReserved stockModel, CancellationToken ct = default)
        {         
            Expression<Func<DeliverySlot, bool>> selectFreeSlotSpec = slot => slot.OrderId == null;
            var freeSlot = await _deliveryRepository.FindAll()
                .Where(selectFreeSlotSpec)
                .OrderBy(s => s.From)
                .FirstOrDefaultAsync(ct);

            if (freeSlot == null)
            {
                await _bus.PubSub.PublishAsync(new NoFreeSlots
                {
                    OrderId = stockModel.OrderId,
                    Products = stockModel.Products
                }, ct);
                return;
            }

            freeSlot.UpdatedDate = DateTime.UtcNow;
            freeSlot.OrderId = stockModel.OrderId;
            await _deliveryRepository.CommitChangesAsync(ct);

            await _bus.PubSub.PublishAsync(new DeliveryReserved
            {
                UserId = stockModel.UserId,
                OrderId = stockModel.OrderId,
                Products = stockModel.Products,
                Cost = stockModel.Cost
            }, ct);
        }

        public async Task ReleaseDelivery(Guid orderId, List<Guid> products, CancellationToken ct = default)
        {
            Expression<Func<DeliverySlot, bool>> selectFreeSlotSpec = slot => slot.OrderId == orderId;
            var existingSlot = await _deliveryRepository.FindAll()
                .Where(selectFreeSlotSpec)
                .SingleOrDefaultAsync(ct);

            existingSlot.UpdatedDate = DateTime.UtcNow;
            existingSlot.OrderId = null;
            await _deliveryRepository.CommitChangesAsync(ct);

            await _bus.PubSub.PublishAsync(new DeliveryReleased
            {
                OrderId = orderId,
                Products = products
            }, ct);
        }

        public async Task<DeliverySlotVm> GetDeliverySlotByOrderId(Guid orderId, CancellationToken ct)
        {
            Expression<Func<DeliverySlot, bool>> selectByOrderIdSpec = slot => slot.OrderId == orderId;
            var existingSlot = await _deliveryRepository.FindAll()
                .Where(selectByOrderIdSpec)
                .SingleOrDefaultAsync(ct);
            if (existingSlot == null)
            {
                throw new KeyNotFoundException($"Delivery slot for an order with Id = '{orderId}' is not found in a database!");
            }

            return existingSlot.ConvertToVm();
        }

        public async Task<List<DeliverySlotVm>> GetDeliverySlots(CancellationToken ct)
        {
            var slots = await _deliveryRepository.FindAll()
                .OrderBy(n => n.From)
                .Select(n => n.ConvertToVm())
                .ToListAsync(ct);

            return slots;
        }
    }
}
