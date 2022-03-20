using System;

namespace Otus.Project.MessageBus.Contracts
{
    public class StockReleased
    {
        public Guid OrderId { get; set; }
    }
}
