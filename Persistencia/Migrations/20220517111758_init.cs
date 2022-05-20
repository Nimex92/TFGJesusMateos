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
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CIF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoCuentaMonetaria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresa", x => x.Id);
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
                name: "SolicitudesVacaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SeAcepta = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesVacaciones", x => x.Id);
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
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
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
                    NumeroTarjeta = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PerteneceATurnos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioIdUser = table.Column<int>(type: "int", nullable: false),
                    Dni = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NumeroSeguridadSocial = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaDeContratacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Categoria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trabajador", x => x.NumeroTarjeta);
                    table.ForeignKey(
                        name: "FK_Trabajador_Usuarios_UsuarioIdUser",
                        column: x => x.UsuarioIdUser,
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
                    TrabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendario_Trabajador_TrabajadorNumeroTarjeta",
                        column: x => x.TrabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EquipoTrabajoTrabajador",
                columns: table => new
                {
                    EquipoId = table.Column<int>(type: "int", nullable: false),
                    TrabajadoresNumeroTarjeta = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EquipoTrabajoTrabajador", x => new { x.EquipoId, x.TrabajadoresNumeroTarjeta });
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTrabajador_EquipoTrabajo_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "EquipoTrabajo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EquipoTrabajoTrabajador_Trabajador_TrabajadoresNumeroTarjeta",
                        column: x => x.TrabajadoresNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Fichajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
                    FechaFichaje = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Entrada_Salida = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fichajes_Trabajador_TrabajadorNumeroTarjeta",
                        column: x => x.TrabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TrabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
                    MotivoIncidencia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaIncidencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Justificada = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Trabajador_TrabajadorNumeroTarjeta",
                        column: x => x.TrabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "NominasTrabajadores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    TrabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
                    HorasNormales = table.Column<int>(type: "int", nullable: false),
                    HorasEspeciales = table.Column<int>(type: "int", nullable: false),
                    TotalAPercibir = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NominasTrabajadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NominasTrabajadores_Empresa_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresa",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NominasTrabajadores_Trabajador_TrabajadorNumeroTarjeta",
                        column: x => x.TrabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
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
                    trabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_TareasComenzadas_Trabajador_trabajadorNumeroTarjeta",
                        column: x => x.trabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
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
                    trabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_TareasFinalizadas_Trabajador_trabajadorNumeroTarjeta",
                        column: x => x.trabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
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
                    Disfrutado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CalendarioPerteneceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiaLibre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiaLibre_Calendario_CalendarioPerteneceId",
                        column: x => x.CalendarioPerteneceId,
                        principalTable: "Calendario",
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
                    trabajadorNumeroTarjeta = table.Column<int>(type: "int", nullable: false),
                    fichajeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrabajadorEnTurno", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrabajadorEnTurno_Fichajes_fichajeId",
                        column: x => x.fichajeId,
                        principalTable: "Fichajes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrabajadorEnTurno_Trabajador_trabajadorNumeroTarjeta",
                        column: x => x.trabajadorNumeroTarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "NumeroTarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_TrabajadorNumeroTarjeta",
                table: "Calendario",
                column: "TrabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_DiaLibre_CalendarioPerteneceId",
                table: "DiaLibre",
                column: "CalendarioPerteneceId");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTareas_TareasIdTarea",
                table: "EquipoTrabajoTareas",
                column: "TareasIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTrabajador_TrabajadoresNumeroTarjeta",
                table: "EquipoTrabajoTrabajador",
                column: "TrabajadoresNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoTurno_TurnosNombre",
                table: "EquipoTrabajoTurno",
                column: "TurnosNombre");

            migrationBuilder.CreateIndex(
                name: "IX_EquipoTrabajoZonas_ZonasIdZona",
                table: "EquipoTrabajoZonas",
                column: "ZonasIdZona");

            migrationBuilder.CreateIndex(
                name: "IX_Fichajes_TrabajadorNumeroTarjeta",
                table: "Fichajes",
                column: "TrabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_TrabajadorNumeroTarjeta",
                table: "Incidencias",
                column: "TrabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_NominasTrabajadores_EmpresaId",
                table: "NominasTrabajadores",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominasTrabajadores_TrabajadorNumeroTarjeta",
                table: "NominasTrabajadores",
                column: "TrabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_tareaIdTarea",
                table: "TareasComenzadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasComenzadas_trabajadorNumeroTarjeta",
                table: "TareasComenzadas",
                column: "trabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_tareaIdTarea",
                table: "TareasFinalizadas",
                column: "tareaIdTarea");

            migrationBuilder.CreateIndex(
                name: "IX_TareasFinalizadas_trabajadorNumeroTarjeta",
                table: "TareasFinalizadas",
                column: "trabajadorNumeroTarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Trabajador_UsuarioIdUser",
                table: "Trabajador",
                column: "UsuarioIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_fichajeId",
                table: "TrabajadorEnTurno",
                column: "fichajeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrabajadorEnTurno_trabajadorNumeroTarjeta",
                table: "TrabajadorEnTurno",
                column: "trabajadorNumeroTarjeta");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiaLibre");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTareas");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTrabajador");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoTurno");

            migrationBuilder.DropTable(
                name: "EquipoTrabajoZonas");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "NominasTrabajadores");

            migrationBuilder.DropTable(
                name: "SolicitudesVacaciones");

            migrationBuilder.DropTable(
                name: "TareasComenzadas");

            migrationBuilder.DropTable(
                name: "TareasFinalizadas");

            migrationBuilder.DropTable(
                name: "TrabajadorEnTurno");

            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "Turno");

            migrationBuilder.DropTable(
                name: "EquipoTrabajo");

            migrationBuilder.DropTable(
                name: "Zonas");

            migrationBuilder.DropTable(
                name: "Empresa");

            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.DropTable(
                name: "Fichajes");

            migrationBuilder.DropTable(
                name: "Trabajador");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
