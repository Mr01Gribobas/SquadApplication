using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadServer.Migrations
{
    /// <inheritdoc />
    public partial class One : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameTeamEnemy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NamePolygon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time", nullable: false),
                    CountMembers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePolygon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CoordinatesPolygon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateEvent = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polygons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinates = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polygons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reantils",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weapon = table.Column<bool>(type: "bit", nullable: false),
                    Mask = table.Column<bool>(type: "bit", nullable: false),
                    Helmet = table.Column<bool>(type: "bit", nullable: false),
                    Balaclava = table.Column<bool>(type: "bit", nullable: false),
                    SVMP = table.Column<bool>(type: "bit", nullable: false),
                    Outterwear = table.Column<bool>(type: "bit", nullable: false),
                    Gloves = table.Column<bool>(type: "bit", nullable: false),
                    BulletproofVestOrUnloadingVest = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reantils", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountMembers = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    _role = table.Column<byte>(type: "tinyint", nullable: false),
                    _callSing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _teamName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _phoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    _userName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    _age = table.Column<int>(type: "int", nullable: true),
                    _isStaffed = table.Column<bool>(type: "bit", nullable: true),
                    _goingToTheGame = table.Column<bool>(type: "bit", nullable: true),
                    EquipmentId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainWeapon = table.Column<bool>(type: "bit", nullable: false),
                    NameMainWeapon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SecondaryWeapon = table.Column<bool>(type: "bit", nullable: false),
                    NameSecondaryWeapon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HeadEquipment = table.Column<bool>(type: "bit", nullable: false),
                    BodyEquipment = table.Column<bool>(type: "bit", nullable: false),
                    UnloudingEquipment = table.Column<bool>(type: "bit", nullable: false),
                    OwnerEquipmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Equipments_Players_OwnerEquipmentId",
                        column: x => x.OwnerEquipmentId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_OwnerEquipmentId",
                table: "Equipments",
                column: "OwnerEquipmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "HistoryEvents");

            migrationBuilder.DropTable(
                name: "Polygons");

            migrationBuilder.DropTable(
                name: "Reantils");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
