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
                name: "DiaLibre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Motivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EsFestivo = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaLibre", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoTrabajo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajo", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TipoEvento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    DescripcionEvento = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                    TiempoEstimado = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.IdTarea);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Turno",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HoraEntrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EsLunes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsMartes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsMiercoles = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsJueves = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsViernes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsSabado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsDomingo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ValidoDesde = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValidoHasta = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Eliminado = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Turno", x => x.Nombre);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Username = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    esAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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
                name: "EquipoTrabajoTareas",
                columns: table => new
                {
                    EquipoTrabajoId = table.Column<int>(type: "int", nullable: false),
                    TareasIdTarea = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajoTareas", x => new { x.EquipoTrabajoId, x.TareasIdTarea });
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTareas_EquipoTrabajo_EquipoTrabajoId",
                        column: x => x.EquipoTrabajoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTareas_Tareas_TareasIdTarea",
                        column: x => x.TareasIdTarea,
                        principalTable: "Tareas",
                        principalColumn: "IdTarea",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoTrabajoTurno",
                columns: table => new
                {
                    EquiposDeTrabajoId = table.Column<int>(type: "int", nullable: false),
                    TurnosNombre = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajoTurno", x => new { x.EquiposDeTrabajoId, x.TurnosNombre });
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTurno_EquipoTrabajo_EquiposDeTrabajoId",
                        column: x => x.EquiposDeTrabajoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTurno_Turno_TurnosNombre",
                        column: x => x.TurnosNombre,
                        principalTable: "Turno",
                        principalColumn: "Nombre",
                        onDelete: ReferentialAction.Cascade);
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
                    perteneceaturnos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    usuarioIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.numero_tarjeta);
                    table.ForeignKey(
                        name: "FK_Trabajador_Usuarios_usuarioIdUser",
                        column: x => x.usuarioIdUser,
                        principalTable: "Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoTrabajoZonas",
                columns: table => new
                {
                    EquiposTrabajoId = table.Column<int>(type: "int", nullable: false),
                    ZonasIdZona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajoZonas", x => new { x.EquiposTrabajoId, x.ZonasIdZona });
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoZonas_EquipoTrabajo_EquiposTrabajoId",
                        column: x => x.EquiposTrabajoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoZonas_Zonas_ZonasIdZona",
                        column: x => x.ZonasIdZona,
                        principalTable: "Zonas",
                        principalColumn: "IdZona",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendario_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoTrabajoTrabajador",
                columns: table => new
                {
                    Trabajadoresnumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    equipoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajoTrabajador", x => new { x.Trabajadoresnumero_tarjeta, x.equipoId });
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTrabajador_EquipoTrabajo_equipoId",
                        column: x => x.equipoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTrabajador_Trabajador_Trabajadoresnumero_tarjeta",
                        column: x => x.Trabajadoresnumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
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
                    EquipoTrabajoId = table.Column<int>(type: "int", nullable: false),
                    FechaFichaje = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Entrada_Salida = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TablaFichajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TablaFichajes_EquipoTrabajo_EquipoTrabajoId",
                        column: x => x.EquipoTrabajoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TablaFichajes_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TareasComenzadas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    tareaIdTarea = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    InicioTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasComenzadas", x => x.id);
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
                    inicioTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FinTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorasUsadas = table.Column<double>(type: "double", nullable: false),
                    EnHora = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TareasFinalizadas", x => x.id);
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

            migrationBuilder.CreateTable(
                name: "CalendarioDiaLibre",
                columns: table => new
                {
                    CalendarioPerteneceId = table.Column<int>(type: "int", nullable: false),
                    DiasDelCalendarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarioDiaLibre", x => new { x.CalendarioPerteneceId, x.DiasDelCalendarioId });
                    table.ForeignKey(
                        name: "FK_CalendarioDiaLibre_Calendario_CalendarioPerteneceId",
                        column: x => x.CalendarioPerteneceId,
                        principalTable: "Calendario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CalendarioDiaLibre_DiaLibre_DiasDelCalendarioId",
                        column: x => x.DiasDelCalendarioId,
                        principalTable: "DiaLibre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrabajadorEnTurno",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    fichajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadorEnTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadorEnTurno_TablaFichajes_fichajeId",
                        column: x => x.fichajeId,
                        principalTable: "TablaFichajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadorEnTurno_Trabajador_trabajadornumero_tarjeta",
                        column: x => x.trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_Trabajadornumero_tarjeta",
                table: "Calendario",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_CalendarioDiaLibre_DiasDelCalendarioId",
                table: "CalendarioDiaLibre",
                column: "DiasDelCalendarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTareas_TareasIdTarea",
                table: "EquipoTrabajoTareas",
                column: "TareasIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTrabajador_equipoId",
                table: "EquipoTrabajoTrabajador",
                column: "equipoId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTurno_TurnosNombre",
                table: "EquipoTrabajoTurno",
                column: "TurnosNombre");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoZonas_ZonasIdZona",
                table: "EquipoTrabajoZonas",
                column: "ZonasIdZona");

            migrationBuilder.CreateIndex(
                name: "IX_TablaFichajes_EquipoTrabajoId",
                table: "TablaFichajes",
                column: "EquipoTrabajoId");

            migrationBuilder.CreateIndex(
                name: "IX_TablaFichajes_Trabajadornumero_tarjeta",
                table: "TablaFichajes",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_tareaIdTarea",
                table: "TareasComenzadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_trabajadornumero_tarjeta",
                table: "TareasComenzadas",
                column: "trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_tareaIdTarea",
                table: "TareasFinalizadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_trabajadornumero_tarjeta",
                table: "TareasFinalizadas",
                column: "trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_usuarioIdUser",
                table: "Trabajador",
                column: "usuarioIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_fichajeId",
                table: "TrabajadorEnTurno",
                column: "fichajeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_trabajadornumero_tarjeta",
                table: "TrabajadorEnTurno",
                column: "trabajadornumero_tarjeta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalendarioDiaLibre");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTareas");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTrabajador");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTurno");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoZonas");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "TareasComenzadas");

            migrationBuilder.DropTable(
                name: "TareasFinalizadas");

            migrationBuilder.DropTable(
                name: "TrabajadorEnTurno");

            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "DiaLibre");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "TablaFichajes");

            migrationBuilder.DropTable(
                name: "EquipoTrabajo");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
