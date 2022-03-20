using System;
using System.Collections.Generic;

namespace Otus.Project.MessageBus.Contracts
{
    public class DeliveryReserved
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public List<Guid> Products { get; set; }

        public decimal Cost { get; set; }
    }
}
