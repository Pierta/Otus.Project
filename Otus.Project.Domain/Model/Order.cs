using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Project.Domain.Model
{
    public class Order : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public decimal Cost { get; set; }

        public bool IsPaid { get; set; }

        [InverseProperty(nameof(OrderProducts.Order))]
        public List<OrderProducts> Products { get; set; }
    }
}
