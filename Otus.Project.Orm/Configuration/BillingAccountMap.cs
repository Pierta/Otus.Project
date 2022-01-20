using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.Orm.Configuration
{
    public class BillingAccountMap : IEntityTypeConfiguration<BillingAccount>
    {
        private readonly Guid BillingAccount1Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac1");
        private readonly Guid BillingAccount2Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac2");
        private readonly Guid BillingAccount3Id = new("0796f40a-9e24-46ea-a7a5-ea445d2d4ac3");

        public void Configure(EntityTypeBuilder<BillingAccount> builder)
        {
            builder.ToTable("BillingAccounts").HasKey(i => i.Id);
            builder.HasIndex(b => b.UserId).IsUnique();

            var now = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasData(new BillingAccount[]
            {
                new BillingAccount
                {
                    Id = BillingAccount1Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    UserId = Constants.TestUser1Id,
                    Balance = 102m
                },
                new BillingAccount
                {
                    Id = BillingAccount2Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    UserId = Constants.TestUser2Id,
                    Balance = 1020m
                },
                new BillingAccount
                {
                    Id = BillingAccount3Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    UserId = Constants.TestUser3Id,
                    Balance = 534m
                }
            });
        }
    }
}
