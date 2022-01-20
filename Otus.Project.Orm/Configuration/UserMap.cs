using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Otus.Project.Domain.Model;
using System;

namespace Otus.Project.Orm.Configuration
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users").HasKey(i => i.Id);

            var now = new DateTime(2022, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            builder.HasData(new User[]
            {
                new User
                {
                    Id = Constants.TestUser1Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    FirstName = "John",
                    LastName = "Dow",
                    MiddleName = "1st",
                    Email = "john.dow.1@example.com",
                    CellPhone = "1111-111-111",
                    IsEmailNotificationEnabled = true,
                    Password = "some_pass_1"
                },
                new User
                {
                    Id = Constants.TestUser2Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    FirstName = "John",
                    LastName = "Dow",
                    MiddleName = "2nd",
                    Email = "john.dow.2@example.com",
                    CellPhone = "1111-222-222",
                    IsEmailNotificationEnabled = true,
                    Password = "some_pass_2"
                },
                new User
                {
                    Id = Constants.TestUser3Id,
                    CreatedDate = now,
                    UpdatedDate = now,
                    FirstName = "John",
                    LastName = "Dow",
                    MiddleName = "3rd",
                    Email = "john.dow.3@example.com",
                    CellPhone = "1111-333-333",
                    IsEmailNotificationEnabled = true,
                    Password = "some_pass_3"
                }
            });
        }
    }
}
