using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class FFX12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TablaFichajes_EquipoTrabajo_EquipoTrabajoId",
                table: "TablaFichajes");

            migrationBuilder.DropIndex(
                name: "IX_TablaFichajes_EquipoTrabajoId",
                table: "TablaFichajes");

            migrationBuilder.DropColumn(
                name: "EquipoTrabajoId",
                table: "TablaFichajes");

            migrationBuilder.AddColumn<bool>(
                name: "Activo",
                table: "Turno",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activo",
                table: "Turno");

            migrationBuilder.AddColumn<int>(
                name: "EquipoTrabajoId",
                table: "TablaFichajes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TablaFichajes_EquipoTrabajoId",
                table: "TablaFichajes",
                column: "EquipoTrabajoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TablaFichajes_EquipoTrabajo_EquipoTrabajoId",
                table: "TablaFichajes",
                column: "EquipoTrabajoId",
                principalTable: "EquipoTrabajo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
