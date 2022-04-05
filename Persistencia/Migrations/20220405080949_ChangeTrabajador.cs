using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class ChangeTrabajador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "usuarioIdUser",
                table: "Trabajador",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_usuarioIdUser",
                table: "Trabajador",
                column: "usuarioIdUser");

            migrationBuilder.AddForeignKey(
                name: "FK_Trabajador_Usuarios_usuarioIdUser",
                table: "Trabajador",
                column: "usuarioIdUser",
                principalTable: "Usuarios",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trabajador_Usuarios_usuarioIdUser",
                table: "Trabajador");

            migrationBuilder.DropIndex(
                name: "IX_Trabajador_usuarioIdUser",
                table: "Trabajador");

            migrationBuilder.DropColumn(
                name: "usuarioIdUser",
                table: "Trabajador");
        }
    }
}
