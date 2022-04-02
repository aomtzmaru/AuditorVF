using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class updatelogmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdminEditLog",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "AdminLog",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "DetailLog",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "EditLog",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "ListLog",
                table: "Log");

            migrationBuilder.RenameColumn(
                name: "ServiceLog",
                table: "Log",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "RegisLog",
                table: "Log",
                newName: "PageAction");

            migrationBuilder.RenameColumn(
                name: "LoginLog",
                table: "Log",
                newName: "ActionDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "Log",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Log");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Log",
                newName: "ServiceLog");

            migrationBuilder.RenameColumn(
                name: "PageAction",
                table: "Log",
                newName: "RegisLog");

            migrationBuilder.RenameColumn(
                name: "ActionDetail",
                table: "Log",
                newName: "LoginLog");

            migrationBuilder.AddColumn<string>(
                name: "AdminEditLog",
                table: "Log",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdminLog",
                table: "Log",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DetailLog",
                table: "Log",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditLog",
                table: "Log",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ListLog",
                table: "Log",
                type: "TEXT",
                nullable: true);
        }
    }
}
