using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;

namespace Otus.Project.Orm.Configuration
{
    public class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable("Notifications").HasKey(i => i.Id);
            builder.HasIndex(b => b.UserId);
            builder.HasIndex(b => b.OrderId);
        }
    }
}
