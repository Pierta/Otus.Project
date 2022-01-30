using System;

namespace Otus.Project.OrderApi.Model
{
    public class ProductVm
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }

        public decimal Cost { get; set; }
    }
}