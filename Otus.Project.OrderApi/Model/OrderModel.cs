using System;
using System.Collections.Generic;

namespace Otus.Project.OrderApi.Model
{
    public class OrderModel
    {
        public Guid? IdempotencyKey { get; set; }
        
        public List<Guid> Products { get; set; }
    }
}
