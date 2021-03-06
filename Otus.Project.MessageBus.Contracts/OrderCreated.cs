using System;
using System.Collections.Generic;

namespace Otus.Project.MessageBus.Contracts
{
    public class OrderCreated
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public List<Guid> Products { get; set; }

        public decimal Cost { get; set; }

        public string RecipientEmail { get; set; }

        public string Message { get; set; }
    }
}
