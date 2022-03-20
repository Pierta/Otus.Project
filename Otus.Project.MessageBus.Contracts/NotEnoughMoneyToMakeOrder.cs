using System;

namespace Otus.Project.MessageBus.Contracts
{
    public class NotEnoughMoneyToMakeOrder
    {
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public string RecipientEmail { get; set; }

        public string Message { get; set; }
    }
}
