using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Project.Domain.Model
{
    public class BillingAccount : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public decimal Balance { get; set; }
    }
}
