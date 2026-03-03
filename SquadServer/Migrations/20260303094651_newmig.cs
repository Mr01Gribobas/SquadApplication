using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SquadServer.Migrations
{
    /// <inheritdoc />
    public partial class newmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Equipments_Players_OwnerEquipmentId",
                table: "Equipments");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateAndTimeGame",
                table: "EventsForAllCommands",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Players_EquipmentId",
                table: "Players",
                column: "EquipmentId",
                unique: true,
                filter: "[EquipmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Equipments_EquipmentId",
                table: "Players",
                column: "EquipmentId",
                principalTable: "Equipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Equipments_EquipmentId",
                table: "Players");

            migrationBuilder.DropIndex(
                name: "IX_Players_EquipmentId",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "DateAndTimeGame",
                table: "EventsForAllCommands");

            migrationBuilder.AddForeignKey(
                name: "FK_Equipments_Players_OwnerEquipmentId",
                table: "Equipments",
                column: "OwnerEquipmentId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
