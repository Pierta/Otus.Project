using System;

namespace Otus.Project.DeliveryApi.Model
{
    public class DeliverySlotVm
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid? OrderId { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Courier { get; set; }

        public bool IsCompleted { get; set; }
    }
}
