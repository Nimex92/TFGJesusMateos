using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Grupo_Trabajo",
                columns: table => new
                {
                    IdGrupo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Turno = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraEntrada = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraSalida = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupo_Trabajo", x => x.IdGrupo);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trabajador",
                columns: table => new
                {
                    numero_tarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    grupoIdGrupo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.numero_tarjeta);
                    table.ForeignKey(
                        name: "FK_Trabajador_Grupo_Trabajo_grupoIdGrupo",
                        column: x => x.grupoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TablaFichajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    Grupo_TrabajoIdGrupo = table.Column<int>(type: "int", nullable: false),
                    FechaFichaje = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaFichajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaFichajes_Grupo_Trabajo_Grupo_TrabajoIdGrupo",
                        column: x => x.Grupo_TrabajoIdGrupo,
                        principalTable: "Grupo_Trabajo",
                        principalColumn: "IdGrupo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TablaFichajes_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_TablaFichajes_Grupo_TrabajoIdGrupo",
                table: "TablaFichajes",
                column: "Grupo_TrabajoIdGrupo");

            migrationBuilder.CreateIndex(
                name: "IX_TablaFichajes_Trabajadornumero_tarjeta",
                table: "TablaFichajes",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_grupoIdGrupo",
                table: "Trabajador",
                column: "grupoIdGrupo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TablaFichajes");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Grupo_Trabajo");
        }
    }
}
