using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.Orm.Configuration
{
    public class StockMap : IEntityTypeConfiguration<Stock>
    {
        public void Configure(EntityTypeBuilder<Stock> builder)
        {
            builder.ToTable("Stocks").HasKey(i => i.Id);

            var now = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasData(new Stock[]
            {
                new Stock
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb1"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    ProductId = ProductMap.Product1Id,
                    Amount = 50
                },
                new Stock
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb2"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    ProductId = ProductMap.Product2Id,
                    Amount = 75
                },
                new Stock
                {
                    Id = new("0796f40a-9e24-46ea-a7a5-ea445d2dbbb3"),
                    CreatedDate = now,
                    UpdatedDate = now,
                    ProductId = ProductMap.Product3Id,
                    Amount = 90
                }
            });
        }
    }
}
