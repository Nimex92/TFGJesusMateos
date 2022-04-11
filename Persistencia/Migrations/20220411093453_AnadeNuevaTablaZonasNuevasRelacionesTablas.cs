using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class AnadeNuevaTablaZonasNuevasRelacionesTablas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zonas",
                columns: table => new
                {
                    IdZona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zonas", x => x.IdZona);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grupo_TrabajoZonas",
                columns: table => new
                {
                    GruposTrabajoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    ZonasIdZona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_TrabajoZonas", x => new { x.GruposTrabajoIdGrupo, x.ZonasIdZona });
                    table.ForeignKey(
                        name: "FK_Grupo_TrabajoZonas_Grupo_Trabajo_GruposTrabajoIdGrupo",
                        column: x => x.GruposTrabajoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupo_TrabajoZonas_Zonas_ZonasIdZona",
                        column: x => x.ZonasIdZona,
                        principalTable: "Zonas",
                        principalColumn: "IdZona",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_TrabajoZonas_ZonasIdZona",
                table: "Grupo_TrabajoZonas",
                column: "ZonasIdZona");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupo_TrabajoZonas");

            migrationBuilder.DropTable(
                name: "Zonas");
        }
    }
}
