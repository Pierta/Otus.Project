using System;

namespace Otus.Project.NotificationApi.Model
{
    public class NotificationVm
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string EventType { get; set; }

        public Guid UserId { get; set; }

        public string RecipientEmail { get; set; }

        public Guid OrderId { get; set; }

        public decimal OrderCost { get; set; }

        public bool OrderIsPaid { get; set; }

        public string Message { get; set; }
    }
}
