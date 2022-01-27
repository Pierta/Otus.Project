using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Project.Orm.Migrations
{
    public partial class AddBillingAccountTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BillingAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Balance = table.Column<decimal>(type: "numeric", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillingAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BillingAccounts",
                columns: new[] { "Id", "Balance", "CreatedDate", "UpdatedDate", "UserId" },
                values: new object[,]
                {
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac1"), 102m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae1") },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac2"), 1020m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae2") },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac3"), 534m, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae3") }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae1"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae2"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae3"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.CreateIndex(
                name: "IX_BillingAccounts_UserId",
                table: "BillingAccounts",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillingAccounts");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae1"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae2"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ae3"),
                columns: new[] { "CreatedDate", "UpdatedDate" },
                values: new object[] { new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810), new DateTime(2021, 11, 9, 17, 14, 23, 981, DateTimeKind.Local).AddTicks(8810) });
        }
    }
}
