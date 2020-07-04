using Microsoft.EntityFrameworkCore.Migrations;

namespace DemoAspNetCoreWebAPI.Migrations
{
    public partial class DateColDropped : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PublishedDate",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
