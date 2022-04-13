using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class DivideTareasRealizadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareasRealizadas");

            migrationBuilder.CreateTable(
                name: "TareasComenzadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tareaIdTarea = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    grupoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    InicioTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasComenzadas", x => x.id);
                    table.ForeignKey(
                        name: "FK_TareasComenzadas_Grupo_Trabajo_grupoIdGrupo",
                        column: x => x.grupoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasComenzadas_Tareas_tareaIdTarea",
                        column: x => x.tareaIdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasComenzadas_Trabajador_trabajadornumero_tarjeta",
                        column: x => x.trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TareasFinalizadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tareaIdTarea = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    grupoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    FinTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorasUsadas = table.Column<double>(type: "double", nullable: false),
                    EnHora = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasFinalizadas", x => x.id);
                    table.ForeignKey(
                        name: "FK_TareasFinalizadas_Grupo_Trabajo_grupoIdGrupo",
                        column: x => x.grupoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasFinalizadas_Tareas_tareaIdTarea",
                        column: x => x.tareaIdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TareasFinalizadas_Trabajador_trabajadornumero_tarjeta",
                        column: x => x.trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_grupoIdGrupo",
                table: "TareasComenzadas",
                column: "grupoIdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_tareaIdTarea",
                table: "TareasComenzadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_trabajadornumero_tarjeta",
                table: "TareasComenzadas",
                column: "trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_grupoIdGrupo",
                table: "TareasFinalizadas",
                column: "grupoIdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_tareaIdTarea",
                table: "TareasFinalizadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_trabajadornumero_tarjeta",
                table: "TareasFinalizadas",
                column: "trabajadornumero_tarjeta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TareasComenzadas");

            migrationBuilder.DropTable(
                name: "TareasFinalizadas");

            migrationBuilder.CreateTable(
                name: "TareasRealizadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    grupoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    tareaIdTarea = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    EnHora = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EnProceso = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FinTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorasUsadas = table.Column<double>(type: "double", nullable: false),
                    InicioTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
    }
}
