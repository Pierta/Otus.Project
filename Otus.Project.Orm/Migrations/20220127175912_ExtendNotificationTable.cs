using Microsoft.EntityFrameworkCore.Migrations;

namespace Otus.Project.Orm.Migrations
{
    public partial class ExtendNotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RecipientEmail",
                table: "Notifications",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecipientEmail",
                table: "Notifications");
        }
    }
}
