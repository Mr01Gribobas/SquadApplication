using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadServer.Migrations
{
    /// <inheritdoc />
    public partial class MSInew : Migration
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
                    CountMembers = table.Column<int>(type: "int", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsForAllCommands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamNameOrganization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TeamIdOrganization = table.Column<int>(type: "int", nullable: false),
                    DescriptionShort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DescriptionFull = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoordinatesPolygon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolygonName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsForAllCommands", x => x.Id);
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
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountMembers = table.Column<int>(type: "int", nullable: false),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    _dataRegistr = table.Column<DateTime>(type: "datetime2", nullable: false),
                    _enterCode = table.Column<long>(type: "bigint", nullable: false),
                    _goingToTheGame = table.Column<bool>(type: "bit", nullable: true),
                    EquipmentId = table.Column<int>(type: "int", nullable: true),
                    TeamId = table.Column<int>(type: "int", nullable: true),
                    StatisticId = table.Column<int>(type: "int", nullable: true),
                    EventsForAllCommandsModelEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_EventsForAllCommands_EventsForAllCommandsModelEntityId",
                        column: x => x.EventsForAllCommandsModelEntityId,
                        principalTable: "EventsForAllCommands",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
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
                    BulletproofVestOrUnloadingVest = table.Column<bool>(type: "bit", nullable: false),
                    IsStaffed = table.Column<bool>(type: "bit", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reantils", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reantils_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceRegistartionModelEntities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DevicePlatform = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastActiveAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    InstallationId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRegistartionModelEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceRegistartionModelEntities_Players_UserId",
                        column: x => x.UserId,
                        principalTable: "Players",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RelatedUserId = table.Column<int>(type: "int", nullable: true),
                    DataJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Notifications_Players_UserId",
                        column: x => x.UserId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PlayerStatistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePlayer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CallSingPlayer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountKill = table.Column<int>(type: "int", nullable: false),
                    CountDieds = table.Column<int>(type: "int", nullable: false),
                    CountFees = table.Column<int>(type: "int", nullable: false),
                    CountEvents = table.Column<int>(type: "int", nullable: false),
                    LastUpdateDataStatistics = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldDataJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AchievementsJson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCommanderCheck = table.Column<bool>(type: "bit", nullable: false),
                    UserModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayerStatistics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayerStatistics_Players_UserModelId",
                        column: x => x.UserModelId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRegistartionModelEntities_UserId",
                table: "DeviceRegistartionModelEntities",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Equipments_OwnerEquipmentId",
                table: "Equipments",
                column: "OwnerEquipmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_EventId",
                table: "Notifications",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_EventsForAllCommandsModelEntityId",
                table: "Players",
                column: "EventsForAllCommandsModelEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayerStatistics_UserModelId",
                table: "PlayerStatistics",
                column: "UserModelId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reantils_TeamId",
                table: "Reantils",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_EventId",
                table: "Teams",
                column: "EventId",
                unique: true,
                filter: "[EventId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceRegistartionModelEntities");

            migrationBuilder.DropTable(
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "HistoryEvents");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "PlayerStatistics");

            migrationBuilder.DropTable(
                name: "Polygons");

            migrationBuilder.DropTable(
                name: "Reantils");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "EventsForAllCommands");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
