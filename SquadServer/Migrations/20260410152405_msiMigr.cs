using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SquadServer.Migrations
{
    /// <inheritdoc />
    public partial class msiMigr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Equipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MainWeapon = table.Column<bool>(type: "boolean", nullable: false),
                    NameMainWeapon = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SecondaryWeapon = table.Column<bool>(type: "boolean", nullable: false),
                    NameSecondaryWeapon = table.Column<string>(type: "text", nullable: true),
                    HeadEquipment = table.Column<bool>(type: "boolean", nullable: false),
                    BodyEquipment = table.Column<bool>(type: "boolean", nullable: false),
                    UnloudingEquipment = table.Column<bool>(type: "boolean", nullable: false),
                    OwnerEquipmentId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameTeamEnemy = table.Column<string>(type: "text", nullable: true),
                    NamePolygon = table.Column<string>(type: "text", nullable: false),
                    Coordinates = table.Column<string>(type: "text", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CountMembers = table.Column<int>(type: "integer", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventsForAllCommands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameGame = table.Column<string>(type: "text", nullable: false),
                    TeamNameOrganization = table.Column<string>(type: "text", nullable: false),
                    TeamIdOrganization = table.Column<int>(type: "integer", nullable: false),
                    DescriptionShort = table.Column<string>(type: "text", nullable: false),
                    DescriptionFull = table.Column<string>(type: "text", nullable: true),
                    CoordinatesPolygon = table.Column<string>(type: "text", nullable: false),
                    PolygonName = table.Column<string>(type: "text", nullable: true),
                    DateAndTimeGame = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsForAllCommands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamePolygon = table.Column<string>(type: "text", nullable: false),
                    CoordinatesPolygon = table.Column<string>(type: "text", nullable: false),
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Coordinates = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polygons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CountMembers = table.Column<int>(type: "integer", nullable: false),
                    EventId = table.Column<int>(type: "integer", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    _role = table.Column<byte>(type: "smallint", nullable: false),
                    _callSing = table.Column<string>(type: "text", nullable: false),
                    _teamName = table.Column<string>(type: "text", nullable: false),
                    _phoneNumber = table.Column<string>(type: "text", nullable: false),
                    _userName = table.Column<string>(type: "text", nullable: true),
                    _age = table.Column<int>(type: "integer", nullable: true),
                    _isStaffed = table.Column<bool>(type: "boolean", nullable: true),
                    _dataRegistr = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    _enterCode = table.Column<long>(type: "bigint", nullable: false),
                    _goingToTheGame = table.Column<bool>(type: "boolean", nullable: true),
                    EquipmentId = table.Column<int>(type: "integer", nullable: true),
                    TeamId = table.Column<int>(type: "integer", nullable: true),
                    StatisticId = table.Column<int>(type: "integer", nullable: true),
                    EventsForAllCommandsModelEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Equipments_EquipmentId",
                        column: x => x.EquipmentId,
                        principalTable: "Equipments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Players_EventsForAllCommands_EventsForAllCommandsModelEntit~",
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Weapon = table.Column<bool>(type: "boolean", nullable: false),
                    Mask = table.Column<bool>(type: "boolean", nullable: false),
                    Helmet = table.Column<bool>(type: "boolean", nullable: false),
                    Balaclava = table.Column<bool>(type: "boolean", nullable: false),
                    SVMP = table.Column<bool>(type: "boolean", nullable: false),
                    Outterwear = table.Column<bool>(type: "boolean", nullable: false),
                    Gloves = table.Column<bool>(type: "boolean", nullable: false),
                    BulletproofVestOrUnloadingVest = table.Column<bool>(type: "boolean", nullable: false),
                    IsStaffed = table.Column<bool>(type: "boolean", nullable: false),
                    TeamId = table.Column<int>(type: "integer", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DeviceToken = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DevicePlatform = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastActiveAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    InstallationId = table.Column<string>(type: "text", nullable: false)
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
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    RelatedUserId = table.Column<int>(type: "integer", nullable: true),
                    DataJson = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    IsSent = table.Column<bool>(type: "boolean", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: true),
                    EventId = table.Column<int>(type: "integer", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NamePlayer = table.Column<string>(type: "text", nullable: false),
                    CallSingPlayer = table.Column<string>(type: "text", nullable: false),
                    CountKill = table.Column<int>(type: "integer", nullable: false),
                    CountDieds = table.Column<int>(type: "integer", nullable: false),
                    CountFees = table.Column<int>(type: "integer", nullable: false),
                    CountEvents = table.Column<int>(type: "integer", nullable: false),
                    LastUpdateDataStatistics = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    OldDataJson = table.Column<string>(type: "text", nullable: true),
                    DataRegistr = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    RoleUser = table.Column<byte>(type: "smallint", nullable: false),
                    AchievementsJson = table.Column<string>(type: "text", nullable: true),
                    IsCommanderCheck = table.Column<bool>(type: "boolean", nullable: false),
                    UserModelId = table.Column<int>(type: "integer", nullable: false)
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
                name: "IX_Players_EquipmentId",
                table: "Players",
                column: "EquipmentId",
                unique: true);

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
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceRegistartionModelEntities");

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
                name: "Equipments");

            migrationBuilder.DropTable(
                name: "EventsForAllCommands");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
