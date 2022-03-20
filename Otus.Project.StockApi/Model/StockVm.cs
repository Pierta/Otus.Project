using System;

namespace Otus.Project.StockApi.Model
{
    public class StockVm
    {
        public Guid Id { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public Guid ProductId { get; set; }

        public string ProductName { get; set; }

        public int Amount { get; set; }
    }
}
