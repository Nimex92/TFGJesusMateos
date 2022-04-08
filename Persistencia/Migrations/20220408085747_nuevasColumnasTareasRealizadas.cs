using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class nuevasColumnasTareasRealizadas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<TimeOnly>(
                name: "FinTarea",
                table: "TareasRealizadas",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "HorasUsadas",
                table: "TareasRealizadas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "InicioTarea",
                table: "TareasRealizadas",
                type: "time(6)",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FinTarea",
                table: "TareasRealizadas");

            migrationBuilder.DropColumn(
                name: "HorasUsadas",
                table: "TareasRealizadas");

            migrationBuilder.DropColumn(
                name: "InicioTarea",
                table: "TareasRealizadas");
        }
    }
}
