using System;
using System.Collections.Generic;

namespace Otus.Project.MessageBus.Contracts
{
    public class PaymentRejected
    {
        public Guid OrderId { get; set; }

        public List<Guid> Products { get; set; }
    }
}
