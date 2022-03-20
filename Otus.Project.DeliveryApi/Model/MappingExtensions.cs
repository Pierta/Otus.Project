using Otus.Project.Domain.Model;

namespace Otus.Project.DeliveryApi.Model
{
    public static class MappingExtensions
    {
        public static DeliverySlotVm ConvertToVm(this DeliverySlot deliverySlot)
        {
            return new DeliverySlotVm
            {
                Id = deliverySlot.Id,
                CreatedDate = deliverySlot.CreatedDate,
                UpdatedDate = deliverySlot.UpdatedDate,
                OrderId = deliverySlot.OrderId,
                From = deliverySlot.From,
                To = deliverySlot.To,
                Courier = deliverySlot.Courier,
                IsCompleted = deliverySlot.IsCompleted
            };
        }
    }
}
