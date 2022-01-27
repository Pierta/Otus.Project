using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.Orm.Configuration
{
    public class ProductMap : IEntityTypeConfiguration<Product>
    {
        private readonly Guid Product1Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac1");
        private readonly Guid Product2Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac2");
        private readonly Guid Product3Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac3");

        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(i => i.Id);

            var now = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasData(new Product[]
            {
                new Product
                {
                    Id = Product1Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    Name = "Product #1",
                    Cost = 55m
                },
                new Product
                {
                    Id = Product2Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    Name = "Product #2",
                    Cost = 1000m
                },
                new Product
                {
                    Id = Product3Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    Name = "Product #3",
                    Cost = 135m
                }
            });
        }
    }
}
