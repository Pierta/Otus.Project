using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.Orm.Configuration
{
    public class DeliverySlotMap : IEntityTypeConfiguration<DeliverySlot>
    {
        public void Configure(EntityTypeBuilder<DeliverySlot> builder)
        {
            builder.ToTable("DeliverySlots").HasKey(i => i.Id);

            var now = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasData(new DeliverySlot[]
            {
                new DeliverySlot
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb1"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    Courier = "Boris Razor",
                    From = new DateTime(2022, 1, 1, 14, 0, 0, DateTimeKind.Unspecified),
                    To = new DateTime(2022, 1, 1, 15, 0, 0, DateTimeKind.Unspecified)
                },
                new DeliverySlot
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb2"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    Courier = "Ivan Grozny",
                    From = new DateTime(2022, 1, 1, 15, 0, 0, DateTimeKind.Unspecified),
                    To = new DateTime(2022, 1, 1, 16, 0, 0, DateTimeKind.Unspecified)
                },
                new DeliverySlot
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb3"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    Courier = "John Wick",
                    From = new DateTime(2022, 1, 1, 16, 0, 0, DateTimeKind.Unspecified),
                    To = new DateTime(2022, 1, 1, 17, 0, 0, DateTimeKind.Unspecified)
                }
            });
        }
    }
}
