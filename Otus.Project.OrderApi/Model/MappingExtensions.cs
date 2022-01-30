using Otus.Project.Domain.Model;
using System.Linq;

namespace Otus.Project.OrderApi.Model
{
    public static class MappingExtensions
    {
        public static OrderVm ConvertToVm(this Order order)
        {
            return new OrderVm
            {
                Id = order.Id,
                CreatedDate = order.CreatedDate,
                UpdatedDate = order.UpdatedDate,
                UserId = order.UserId,
                Cost = order.Cost,
                IsPaid = order.IsPaid,
                Products = order.Products?
                    .Select(op => new ProductVm
                    {
                        Id = op.ProductId,
                        Name = op.Product.Name,
                        Cost = op.Product.Cost
                    }).ToList()
            };
        }

        public static ProductVm ConvertToVm(this Product product)
        {
            return new ProductVm
            {
                Id = product.Id,
                Name = product.Name,
                Cost = product.Cost
            };
        }
    }
}
