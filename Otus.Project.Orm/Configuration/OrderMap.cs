using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;

namespace Otus.Project.Orm.Configuration
{
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders").HasKey(i => i.Id);
            builder.HasIndex(b => b.UserId);
        }
    }
}
