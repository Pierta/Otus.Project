using Otus.Project.MessageBus.Contracts;
using Otus.Project.DeliveryApi.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Otus.Project.DeliveryApi.Services
{
    public interface IDeliveryService
    {
        Task ReserveDelivery(StockReserved stockModel, CancellationToken ct = default);

        Task ReleaseDelivery(Guid orderId, List<Guid> products, CancellationToken ct = default);

        Task<DeliverySlotVm> GetDeliverySlotByOrderId(Guid orderId, CancellationToken ct);

        Task<List<DeliverySlotVm>> GetDeliverySlots(CancellationToken ct);
    }
}
