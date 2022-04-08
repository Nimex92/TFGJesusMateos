using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class nuevasColumnasTareasRealizadas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InicioTarea",
                table: "TareasRealizadas",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "FinTarea",
                table: "TareasRealizadas",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(TimeOnly),
                oldType: "time(6)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeOnly>(
                name: "InicioTarea",
                table: "TareasRealizadas",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "FinTarea",
                table: "TareasRealizadas",
                type: "time(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");
        }
    }
}
