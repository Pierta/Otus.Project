using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Project.Orm.Migrations
{
    public partial class ExtendNotificationTable_AddEventType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EventType",
                table: "Notifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventType",
                table: "Notifications");
        }
    }
}
