using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class AnadeDiaCalendarioTablas3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dia_Calendario_CalendarioId",
                table: "Dia");

            migrationBuilder.DropIndex(
                name: "IX_Dia_CalendarioId",
                table: "Dia");

            migrationBuilder.DropColumn(
                name: "CalendarioId",
                table: "Dia");

            migrationBuilder.AddColumn<bool>(
                name: "EsFestivo",
                table: "Dia",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Fecha",
                table: "Dia",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Motivo",
                table: "Dia",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CalendarioDia",
                columns: table => new
                {
                    CalendarioPerteneceId = table.Column<int>(type: "int", nullable: false),
                    DiasDelCalendarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarioDia", x => new { x.CalendarioPerteneceId, x.DiasDelCalendarioId });
                    table.ForeignKey(
                        name: "FK_CalendarioDia_Calendario_CalendarioPerteneceId",
                        column: x => x.CalendarioPerteneceId,
                        principalTable: "Calendario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarioDia_Dia_DiasDelCalendarioId",
                        column: x => x.DiasDelCalendarioId,
                        principalTable: "Dia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarioDia_DiasDelCalendarioId",
                table: "CalendarioDia",
                column: "DiasDelCalendarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarioDia");

            migrationBuilder.DropColumn(
                name: "EsFestivo",
                table: "Dia");

            migrationBuilder.DropColumn(
                name: "Fecha",
                table: "Dia");

            migrationBuilder.DropColumn(
                name: "Motivo",
                table: "Dia");

            migrationBuilder.AddColumn<int>(
                name: "CalendarioId",
                table: "Dia",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dia_CalendarioId",
                table: "Dia",
                column: "CalendarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dia_Calendario_CalendarioId",
                table: "Dia",
                column: "CalendarioId",
                principalTable: "Calendario",
                principalColumn: "Id");
        }
    }
}
