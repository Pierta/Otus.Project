using Otus.Project.Domain.Model;

namespace Otus.Project.StockApi.Model
{
    public static class MappingExtensions
    {
        public static StockVm ConvertToVm(this Stock stock)
        {
            return new StockVm
            {
                Id = stock.Id,
                CreatedDate = stock.CreatedDate,
                UpdatedDate = stock.UpdatedDate,
                ProductId = stock.ProductId,
                ProductName = stock.Product.Name,
                Amount = stock.Amount
            };
        }
    }
}
