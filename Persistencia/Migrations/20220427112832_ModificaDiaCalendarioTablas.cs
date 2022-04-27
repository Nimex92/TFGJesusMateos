using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class ModificaDiaCalendarioTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarioDia");

            migrationBuilder.DropTable(
                name: "Dia");

            migrationBuilder.CreateTable(
                name: "DiaLibre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Motivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EsFestivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaLibre", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "CalendarioDiaLibre",
                columns: table => new
                {
                    CalendarioPerteneceId = table.Column<int>(type: "int", nullable: false),
                    DiasDelCalendarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarioDiaLibre", x => new { x.CalendarioPerteneceId, x.DiasDelCalendarioId });
                    table.ForeignKey(
                        name: "FK_CalendarioDiaLibre_Calendario_CalendarioPerteneceId",
                        column: x => x.CalendarioPerteneceId,
                        principalTable: "Calendario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarioDiaLibre_DiaLibre_DiasDelCalendarioId",
                        column: x => x.DiasDelCalendarioId,
                        principalTable: "DiaLibre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarioDiaLibre_DiasDelCalendarioId",
                table: "CalendarioDiaLibre",
                column: "DiasDelCalendarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarioDiaLibre");

            migrationBuilder.DropTable(
                name: "DiaLibre");

            migrationBuilder.CreateTable(
                name: "Dia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EsFestivo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Motivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dia", x => x.Id);
                })
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
    }
}
