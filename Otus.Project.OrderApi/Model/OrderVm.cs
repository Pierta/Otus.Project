using System;
using System.Collections.Generic;

namespace Otus.Project.OrderApi.Model
{
    public class OrderVm
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid UserId { get; set; }

        public decimal Cost { get; set; }

        public bool IsPaid { get; set; }

        public List<ProductVm> Products { get; set; }
    }
}
