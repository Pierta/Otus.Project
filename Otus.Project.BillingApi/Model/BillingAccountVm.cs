using System;

namespace Otus.Project.BillingApi.Model
{
    public class BillingAccountVm
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid UserId { get; set; }

        public decimal Balance { get; set; }
    }
}
