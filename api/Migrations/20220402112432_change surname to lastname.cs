using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class changesurnametolastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurName",
                table: "User",
                newName: "LastName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "User",
                newName: "SurName");
        }
    }
}
