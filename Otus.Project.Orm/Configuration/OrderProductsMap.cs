using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;

namespace Otus.Project.Orm.Configuration
{
    public class OrderProductsMap : IEntityTypeConfiguration<OrderProducts>
    {
        public void Configure(EntityTypeBuilder<OrderProducts> builder)
        {
            builder.ToTable("OrderProducts").HasKey(i => i.Id);
            builder.HasIndex(b => b.OrderId);
        }
    }
}
