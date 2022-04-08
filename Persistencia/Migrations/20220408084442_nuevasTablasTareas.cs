using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class nuevasTablasTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    IdTarea = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NombreTarea = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TiempoEstimado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.IdTarea);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TareasRealizadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tareaIdTarea = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    grupoIdGrupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasRealizadas", x => x.id);
                    table.ForeignKey(
                        name: "FK_TareasRealizadas_Grupo_Trabajo_grupoIdGrupo",
                        column: x => x.grupoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasRealizadas_Tareas_tareaIdTarea",
                        column: x => x.tareaIdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasRealizadas_Trabajador_trabajadornumero_tarjeta",
                        column: x => x.trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TareasRealizadas_grupoIdGrupo",
                table: "TareasRealizadas",
                column: "grupoIdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_TareasRealizadas_tareaIdTarea",
                table: "TareasRealizadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasRealizadas_trabajadornumero_tarjeta",
                table: "TareasRealizadas",
                column: "trabajadornumero_tarjeta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareasRealizadas");

            migrationBuilder.DropTable(
                name: "Tareas");
        }
    }
}
