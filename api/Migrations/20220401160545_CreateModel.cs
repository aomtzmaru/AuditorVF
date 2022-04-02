using Microsoft.EntityFrameworkCore.Migrations;

namespace api.Migrations
{
    public partial class CreateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdCardDuplicate = table.Column<string>(type: "TEXT", nullable: true),
                    AuditorCardDuplicate = table.Column<string>(type: "TEXT", nullable: true),
                    RenameDuplicate = table.Column<string>(type: "TEXT", nullable: true),
                    PhotoFiles = table.Column<string>(type: "TEXT", nullable: true),
                    LicenceDuplicate = table.Column<string>(type: "TEXT", nullable: true),
                    OtherDuplicate = table.Column<string>(type: "TEXT", nullable: true),
                    MissingDocument = table.Column<string>(type: "TEXT", nullable: true),
                    SlipPayment = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LoginLog = table.Column<string>(type: "TEXT", nullable: true),
                    RegisLog = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceLog = table.Column<string>(type: "TEXT", nullable: true),
                    ListLog = table.Column<string>(type: "TEXT", nullable: true),
                    DetailLog = table.Column<string>(type: "TEXT", nullable: true),
                    EditLog = table.Column<string>(type: "TEXT", nullable: true),
                    AdminLog = table.Column<string>(type: "TEXT", nullable: true),
                    AdminEditLog = table.Column<string>(type: "TEXT", nullable: true)
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
                    RenewServices = table.Column<string>(type: "TEXT", nullable: true),
                    NewCardServices = table.Column<string>(type: "TEXT", nullable: true),
                    ReplaceServices = table.Column<string>(type: "TEXT", nullable: true),
                    PrintServices = table.Column<string>(type: "TEXT", nullable: true)
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
                    SurName = table.Column<string>(type: "TEXT", nullable: true),
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
                    Majority = table.Column<string>(type: "TEXT", nullable: true),
                    Domicile = table.Column<string>(type: "TEXT", nullable: true),
                    Bankrupt = table.Column<string>(type: "TEXT", nullable: true),
                    Insane = table.Column<string>(type: "TEXT", nullable: true),
                    Imprisonment = table.Column<string>(type: "TEXT", nullable: true),
                    Revoke = table.Column<string>(type: "TEXT", nullable: true),
                    Registration = table.Column<string>(type: "TEXT", nullable: true),
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
                    UsernameRegister = table.Column<string>(type: "TEXT", nullable: true),
                    PasswordRegister = table.Column<string>(type: "TEXT", nullable: true),
                    DateRegis = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
