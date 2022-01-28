using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Project.Domain.Model
{
    public class Notification : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string EventType { get; set; }

        public string RecipientEmail { get; set; }

        public string Message { get; set; }
    }
}
