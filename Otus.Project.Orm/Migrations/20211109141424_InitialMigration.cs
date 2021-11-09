using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Project.Orm.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    CellPhone = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    IsEmailNotificationEnabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CellPhone", "CreatedDate", "Email", "FirstName", "IsEmailNotificationEnabled", "LastName", "MiddleName", "Password", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae1"), "1111-111-111", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), "john.dow.1@example.com", "John", true, "Dow", "1st", "some_pass_1", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae2"), "1111-222-222", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), "john.dow.2@example.com", "John", true, "Dow", "2nd", "some_pass_2", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae3"), "1111-333-333", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), "john.dow.3@example.com", "John", true, "Dow", "3rd", "some_pass_3", new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
