using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class AnadeTablaTrabajadorEnTurno2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "fichajeId",
                table: "TrabajadorEnTurno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_fichajeId",
                table: "TrabajadorEnTurno",
                column: "fichajeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno",
                column: "trabajadornumero_tarjeta");

            migrationBuilder.AddForeignKey(
                name: "FK_TrabajadorEnTurno_TablaFichajes_fichajeId",
                table: "TrabajadorEnTurno",
                column: "fichajeId",
                principalTable: "TablaFichajes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrabajadorEnTurno_Trabajador_trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno",
                column: "trabajadornumero_tarjeta",
                principalTable: "Trabajador",
                principalColumn: "numero_tarjeta",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TrabajadorEnTurno_TablaFichajes_fichajeId",
                table: "TrabajadorEnTurno");

            migrationBuilder.DropForeignKey(
                name: "FK_TrabajadorEnTurno_Trabajador_trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno");

            migrationBuilder.DropIndex(
                name: "IX_TrabajadorEnTurno_fichajeId",
                table: "TrabajadorEnTurno");

            migrationBuilder.DropIndex(
                name: "IX_TrabajadorEnTurno_trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno");

            migrationBuilder.DropColumn(
                name: "fichajeId",
                table: "TrabajadorEnTurno");

            migrationBuilder.DropColumn(
                name: "trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno");
        }
    }
}
