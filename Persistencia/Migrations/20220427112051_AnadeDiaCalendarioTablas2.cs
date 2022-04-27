using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class AnadeDiaCalendarioTablas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendario_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Dia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CalendarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dia_Calendario_CalendarioId",
                        column: x => x.CalendarioId,
                        principalTable: "Calendario",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_Trabajadornumero_tarjeta",
                table: "Calendario",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Dia_CalendarioId",
                table: "Dia",
                column: "CalendarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dia");

            migrationBuilder.DropTable(
                name: "Calendario");
        }
    }
}
