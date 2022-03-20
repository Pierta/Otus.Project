using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Project.Domain.Model
{
    public class DeliverySlot : BaseEntity
    {
        [ForeignKey(nameof(Order))]
        public Guid? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public DateTime From { get; set; }

        public DateTime To { get; set; }

        public string Courier { get; set; }

        public bool IsCompleted { get; set; }
    }
}
