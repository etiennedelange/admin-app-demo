using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminApp.Data.Migrations
{
    public partial class SingularizeTableNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoggedEvents",
                table: "LoggedEvents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTitles",
                table: "JobTitles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Errors",
                table: "Errors");

            migrationBuilder.RenameTable(
                name: "LoggedEvents",
                newName: "LoggedEvent");

            migrationBuilder.RenameTable(
                name: "JobTitles",
                newName: "JobTitle");

            migrationBuilder.RenameTable(
                name: "Errors",
                newName: "Error");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoggedEvent",
                table: "LoggedEvent",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTitle",
                table: "JobTitle",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Error",
                table: "Error",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_LoggedEvent",
                table: "LoggedEvent");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobTitle",
                table: "JobTitle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Error",
                table: "Error");

            migrationBuilder.RenameTable(
                name: "LoggedEvent",
                newName: "LoggedEvents");

            migrationBuilder.RenameTable(
                name: "JobTitle",
                newName: "JobTitles");

            migrationBuilder.RenameTable(
                name: "Error",
                newName: "Errors");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoggedEvents",
                table: "LoggedEvents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobTitles",
                table: "JobTitles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Errors",
                table: "Errors",
                column: "Id");
        }
    }
}
