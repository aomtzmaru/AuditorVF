using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class ChangeStructureTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReplaceServices",
                table: "Services",
                newName: "ZipCodeContact");

            migrationBuilder.RenameColumn(
                name: "RenewServices",
                table: "Services",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "PrintServices",
                table: "Services",
                newName: "UpdatedIp");

            migrationBuilder.RenameColumn(
                name: "NewCardServices",
                table: "Services",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "SlipPayment",
                table: "Files",
                newName: "UpdatedUser");

            migrationBuilder.RenameColumn(
                name: "RenameDuplicate",
                table: "Files",
                newName: "UpdatedIp");

            migrationBuilder.RenameColumn(
                name: "PhotoFiles",
                table: "Files",
                newName: "FilePath");

            migrationBuilder.RenameColumn(
                name: "OtherDuplicate",
                table: "Files",
                newName: "FileName");

            migrationBuilder.RenameColumn(
                name: "MissingDocument",
                table: "Files",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "LicenceDuplicate",
                table: "Files",
                newName: "CreatedUser");

            migrationBuilder.RenameColumn(
                name: "IdCardDuplicate",
                table: "Files",
                newName: "CreatedIp");

            migrationBuilder.RenameColumn(
                name: "AuditorCardDuplicate",
                table: "Files",
                newName: "ContentType");

            migrationBuilder.AddColumn<string>(
                name: "CreatedIp",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "User",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedIp",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedUser",
                table: "User",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmphurContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedIp",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUser",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Services",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DistrictContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MobileNumber",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MooContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrefixName",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecieveBranch",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RecieveDoc",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegNumber",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RoadContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ServiceType",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoiContact",
                table: "Services",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Services",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "Log",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Deleted",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Files",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "Files",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedIp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedIp",
                table: "User");

            migrationBuilder.DropColumn(
                name: "UpdatedUser",
                table: "User");

            migrationBuilder.DropColumn(
                name: "AddressContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "AmphurContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedIp",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "CreatedUser",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "DistrictContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MobileNumber",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MooContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "PrefixName",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ProvinceContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RecieveBranch",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RecieveDoc",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RegNumber",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "RoadContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "ServiceType",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "SoiContact",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "IP",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "ZipCodeContact",
                table: "Services",
                newName: "ReplaceServices");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Services",
                newName: "RenewServices");

            migrationBuilder.RenameColumn(
                name: "UpdatedIp",
                table: "Services",
                newName: "PrintServices");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Services",
                newName: "NewCardServices");

            migrationBuilder.RenameColumn(
                name: "UpdatedUser",
                table: "Files",
                newName: "SlipPayment");

            migrationBuilder.RenameColumn(
                name: "UpdatedIp",
                table: "Files",
                newName: "RenameDuplicate");

            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Files",
                newName: "PhotoFiles");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "Files",
                newName: "OtherDuplicate");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Files",
                newName: "MissingDocument");

            migrationBuilder.RenameColumn(
                name: "CreatedUser",
                table: "Files",
                newName: "LicenceDuplicate");

            migrationBuilder.RenameColumn(
                name: "CreatedIp",
                table: "Files",
                newName: "IdCardDuplicate");

            migrationBuilder.RenameColumn(
                name: "ContentType",
                table: "Files",
                newName: "AuditorCardDuplicate");
        }
    }
}
