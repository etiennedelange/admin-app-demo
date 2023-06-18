using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminApp.Data.Migrations
{
    public partial class AddIsHostedFirmColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHostedFirm",
                table: "Attorney",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHostedFirm",
                table: "Attorney");
        }
    }
}
