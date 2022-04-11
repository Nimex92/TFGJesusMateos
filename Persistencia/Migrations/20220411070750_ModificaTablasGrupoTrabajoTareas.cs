using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class ModificaTablasGrupoTrabajoTareas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EnHora",
                table: "TareasRealizadas",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Grupo_TrabajoTareas",
                columns: table => new
                {
                    GruposTrabajoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    TareasIdTarea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_TrabajoTareas", x => new { x.GruposTrabajoIdGrupo, x.TareasIdTarea });
                    table.ForeignKey(
                        name: "FK_Grupo_TrabajoTareas_Grupo_Trabajo_GruposTrabajoIdGrupo",
                        column: x => x.GruposTrabajoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grupo_TrabajoTareas_Tareas_TareasIdTarea",
                        column: x => x.TareasIdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Grupo_TrabajoTareas_TareasIdTarea",
                table: "Grupo_TrabajoTareas",
                column: "TareasIdTarea");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grupo_TrabajoTareas");

            migrationBuilder.DropColumn(
                name: "EnHora",
                table: "TareasRealizadas");
        }
    }
}
