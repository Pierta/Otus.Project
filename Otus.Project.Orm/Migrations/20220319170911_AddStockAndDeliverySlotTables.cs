using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Project.Orm.Migrations
{
    public partial class AddStockAndDeliverySlotTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderState",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DeliverySlots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: true),
                    From = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    To = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Courier = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliverySlots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliverySlots_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DeliverySlots",
                columns: new[] { "Id", "Courier", "CreatedDate", "From", "IsCompleted", "OrderId", "To", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb1"), "Boris Razor", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2022, 1, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb2"), "Ivan Grozny", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 15, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2022, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb3"), "John Wick", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2022, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2022, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.InsertData(
                table: "Stocks",
                columns: new[] { "Id", "Amount", "CreatedDate", "ProductId", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb1"), 50, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac1"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb2"), 75, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac2"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                    { new Guid("0796f40a-9e24-46ea-a7a5-ea445d2dbbb3"), 90, new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), new Guid("0796f40a-9e24-46ea-a7a5-ea445d2d4ac3"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliverySlots_OrderId",
                table: "DeliverySlots",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_ProductId",
                table: "Stocks",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliverySlots");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropColumn(
                name: "OrderState",
                table: "Orders");
        }
    }
}
