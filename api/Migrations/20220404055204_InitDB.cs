using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class InitDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    IP = table.Column<string>(type: "TEXT", nullable: true),
                    ActionDetail = table.Column<string>(type: "TEXT", nullable: true),
                    PageAction = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceType = table.Column<string>(type: "TEXT", nullable: true),
                    PrefixName = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    RegNumber = table.Column<string>(type: "TEXT", nullable: true),
                    RecieveDoc = table.Column<string>(type: "TEXT", nullable: true),
                    RecieveBranch = table.Column<string>(type: "TEXT", nullable: true),
                    AddressContact = table.Column<string>(type: "TEXT", nullable: true),
                    MooContact = table.Column<string>(type: "TEXT", nullable: true),
                    SoiContact = table.Column<string>(type: "TEXT", nullable: true),
                    RoadContact = table.Column<string>(type: "TEXT", nullable: true),
                    DistrictContact = table.Column<string>(type: "TEXT", nullable: true),
                    AmphurContact = table.Column<string>(type: "TEXT", nullable: true),
                    ProvinceContact = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCodeContact = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedUser = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedUser = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PerId = table.Column<string>(type: "TEXT", nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", nullable: true),
                    LastName = table.Column<string>(type: "TEXT", nullable: true),
                    PrefixName = table.Column<string>(type: "TEXT", nullable: true),
                    AuditorVf = table.Column<string>(type: "TEXT", nullable: true),
                    AddressHouse = table.Column<string>(type: "TEXT", nullable: true),
                    MooHouse = table.Column<string>(type: "TEXT", nullable: true),
                    SoiHouse = table.Column<string>(type: "TEXT", nullable: true),
                    RoadHouse = table.Column<string>(type: "TEXT", nullable: true),
                    DistrictHouse = table.Column<string>(type: "TEXT", nullable: true),
                    AmphurHouse = table.Column<string>(type: "TEXT", nullable: true),
                    ProvinceHouse = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCodeHouse = table.Column<string>(type: "TEXT", nullable: true),
                    AddressContact = table.Column<string>(type: "TEXT", nullable: true),
                    MooContact = table.Column<string>(type: "TEXT", nullable: true),
                    SoiContact = table.Column<string>(type: "TEXT", nullable: true),
                    RoadContact = table.Column<string>(type: "TEXT", nullable: true),
                    DistrictContact = table.Column<string>(type: "TEXT", nullable: true),
                    AmphurContact = table.Column<string>(type: "TEXT", nullable: true),
                    ProvinceContact = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCodeContact = table.Column<string>(type: "TEXT", nullable: true),
                    MobileNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    EducateDegree = table.Column<string>(type: "TEXT", nullable: true),
                    Majority = table.Column<bool>(type: "INTEGER", nullable: false),
                    Domicile = table.Column<bool>(type: "INTEGER", nullable: false),
                    Bankrupt = table.Column<bool>(type: "INTEGER", nullable: false),
                    Insane = table.Column<bool>(type: "INTEGER", nullable: false),
                    Imprisonment = table.Column<bool>(type: "INTEGER", nullable: false),
                    Revoke = table.Column<bool>(type: "INTEGER", nullable: false),
                    Registration = table.Column<bool>(type: "INTEGER", nullable: false),
                    Occupation = table.Column<string>(type: "TEXT", nullable: true),
                    WorkPlace = table.Column<string>(type: "TEXT", nullable: true),
                    AddressWork = table.Column<string>(type: "TEXT", nullable: true),
                    MooWork = table.Column<string>(type: "TEXT", nullable: true),
                    SoiWork = table.Column<string>(type: "TEXT", nullable: true),
                    RoadWork = table.Column<string>(type: "TEXT", nullable: true),
                    DistrictWork = table.Column<string>(type: "TEXT", nullable: true),
                    ProvinceWork = table.Column<string>(type: "TEXT", nullable: true),
                    ZipCodeWork = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneWork = table.Column<string>(type: "TEXT", nullable: true),
                    MobileWork = table.Column<string>(type: "TEXT", nullable: true),
                    EmailWork = table.Column<string>(type: "TEXT", nullable: true),
                    ManualVf = table.Column<string>(type: "TEXT", nullable: true),
                    PostDelivery = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    Username = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "BLOB", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deleted = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedUser = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceId = table.Column<int>(type: "INTEGER", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FilePath = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ContentType = table.Column<string>(type: "TEXT", nullable: true),
                    FileStream = table.Column<string>(type: "TEXT", nullable: true),
                    FileId = table.Column<string>(type: "TEXT", nullable: true),
                    EncryptFileName = table.Column<string>(type: "TEXT", nullable: true),
                    Deleted = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedUser = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedIp = table.Column<string>(type: "TEXT", nullable: true),
                    UpdatedUser = table.Column<string>(type: "TEXT", nullable: true),
                    ServicesId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Services_ServicesId",
                        column: x => x.ServicesId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Files_ServicesId",
                table: "Files",
                column: "ServicesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
