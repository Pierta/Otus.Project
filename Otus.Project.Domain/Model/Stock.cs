using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Otus.Project.Domain.Model
{
    public class Stock : BaseEntity
    {
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int Amount { get; set; }
    }
}
