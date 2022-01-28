using System;

namespace Otus.Project.NotificationApi.Contracts
{
    public class NotificationModel
    {        
        public Guid UserId { get; set; }

        public Guid OrderId { get; set; }

        public string RecipientEmail { get; set; }

        public string Message { get; set; }
    }
}
