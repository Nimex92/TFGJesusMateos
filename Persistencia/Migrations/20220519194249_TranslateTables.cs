using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistencia.Migrations
{
    public partial class TranslateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CIF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Adress = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BankAccountCode = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Business", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Places",
                columns: table => new
                {
                    PlaceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Places", x => x.PlaceId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
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
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VacationRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Worker = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Accepted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationRequests", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkShifts",
                columns: table => new
                {
                    Name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CheckIn = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Monday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Tuesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Wednesday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Thursday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Friday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Saturday = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Domingo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValidUntil = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Deleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShifts", x => x.Name);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkTasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ElapsedTime = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTasks", x => x.TaskId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Workers",
                columns: table => new
                {
                    CardNumber = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BelongsToWorkShifts = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workers", x => x.CardNumber);
                    table.ForeignKey(
                        name: "FK_Workers_Users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PlacesWorkGroup",
                columns: table => new
                {
                    PlacesPlaceId = table.Column<int>(type: "int", nullable: false),
                    WorkGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlacesWorkGroup", x => new { x.PlacesPlaceId, x.WorkGroupsId });
                    table.ForeignKey(
                        name: "FK_PlacesWorkGroup_Places_PlacesPlaceId",
                        column: x => x.PlacesPlaceId,
                        principalTable: "Places",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlacesWorkGroup_WorkGroups_WorkGroupsId",
                        column: x => x.WorkGroupsId,
                        principalTable: "WorkGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkGroupWorkShift",
                columns: table => new
                {
                    WorkShiftsId = table.Column<int>(type: "int", nullable: false),
                    WorkShiftsName = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroupWorkShift", x => new { x.WorkShiftsId, x.WorkShiftsName });
                    table.ForeignKey(
                        name: "FK_WorkGroupWorkShift_WorkGroups_WorkShiftsId",
                        column: x => x.WorkShiftsId,
                        principalTable: "WorkGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkGroupWorkShift_WorkShifts_WorkShiftsName",
                        column: x => x.WorkShiftsName,
                        principalTable: "WorkShifts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkGroupWorkTask",
                columns: table => new
                {
                    TasksTaskId = table.Column<int>(type: "int", nullable: false),
                    WorkGroupsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroupWorkTask", x => new { x.TasksTaskId, x.WorkGroupsId });
                    table.ForeignKey(
                        name: "FK_WorkGroupWorkTask_WorkGroups_WorkGroupsId",
                        column: x => x.WorkGroupsId,
                        principalTable: "WorkGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkGroupWorkTask_WorkTasks_TasksTaskId",
                        column: x => x.TasksTaskId,
                        principalTable: "WorkTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Calendars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Calendars_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EndedTasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    TaskTaskInit = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TaskEnd = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalTimeUsed = table.Column<double>(type: "double", nullable: false),
                    OnTime = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndedTasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_EndedTasks_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndedTasks_WorkTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "WorkTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    IssueReason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IssueDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Justified = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Signings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    SigningDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CheckInCheckOut = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signings_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StartedTasks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    TastStart = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartedTasks", x => x.id);
                    table.ForeignKey(
                        name: "FK_StartedTasks_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StartedTasks_WorkTasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "WorkTasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkerPayrolls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    BusinessId = table.Column<int>(type: "int", nullable: false),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    NormalHours = table.Column<int>(type: "int", nullable: false),
                    SpecialHours = table.Column<int>(type: "int", nullable: false),
                    TotalToReceive = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerPayrolls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerPayrolls_Business_BusinessId",
                        column: x => x.BusinessId,
                        principalTable: "Business",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerPayrolls_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "WorkGroupWorker",
                columns: table => new
                {
                    WorkGroupId = table.Column<int>(type: "int", nullable: false),
                    WorkersCardNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkGroupWorker", x => new { x.WorkGroupId, x.WorkersCardNumber });
                    table.ForeignKey(
                        name: "FK_WorkGroupWorker_Workers_WorkersCardNumber",
                        column: x => x.WorkersCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkGroupWorker_WorkGroups_WorkGroupId",
                        column: x => x.WorkGroupId,
                        principalTable: "WorkGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DayOff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Reason = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Enjoyed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    BelongCalendarId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOff", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DayOff_Calendars_BelongCalendarId",
                        column: x => x.BelongCalendarId,
                        principalTable: "Calendars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SignedWorkers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkerCardNumber = table.Column<int>(type: "int", nullable: false),
                    SigningId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SignedWorkers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SignedWorkers_Signings_SigningId",
                        column: x => x.SigningId,
                        principalTable: "Signings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SignedWorkers_Workers_WorkerCardNumber",
                        column: x => x.WorkerCardNumber,
                        principalTable: "Workers",
                        principalColumn: "CardNumber",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Calendars_WorkerCardNumber",
                table: "Calendars",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_DayOff_BelongCalendarId",
                table: "DayOff",
                column: "BelongCalendarId");

            migrationBuilder.CreateIndex(
                name: "IX_EndedTasks_TaskId",
                table: "EndedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_EndedTasks_WorkerCardNumber",
                table: "EndedTasks",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_WorkerCardNumber",
                table: "Issues",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_PlacesWorkGroup_WorkGroupsId",
                table: "PlacesWorkGroup",
                column: "WorkGroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedWorkers_SigningId",
                table: "SignedWorkers",
                column: "SigningId");

            migrationBuilder.CreateIndex(
                name: "IX_SignedWorkers_WorkerCardNumber",
                table: "SignedWorkers",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Signings_WorkerCardNumber",
                table: "Signings",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StartedTasks_TaskId",
                table: "StartedTasks",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_StartedTasks_WorkerCardNumber",
                table: "StartedTasks",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerPayrolls_BusinessId",
                table: "WorkerPayrolls",
                column: "BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerPayrolls_WorkerCardNumber",
                table: "WorkerPayrolls",
                column: "WorkerCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Workers_UserIdUser",
                table: "Workers",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupWorker_WorkersCardNumber",
                table: "WorkGroupWorker",
                column: "WorkersCardNumber");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupWorkShift_WorkShiftsName",
                table: "WorkGroupWorkShift",
                column: "WorkShiftsName");

            migrationBuilder.CreateIndex(
                name: "IX_WorkGroupWorkTask_WorkGroupsId",
                table: "WorkGroupWorkTask",
                column: "WorkGroupsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOff");

            migrationBuilder.DropTable(
                name: "EndedTasks");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "PlacesWorkGroup");

            migrationBuilder.DropTable(
                name: "SignedWorkers");

            migrationBuilder.DropTable(
                name: "StartedTasks");

            migrationBuilder.DropTable(
                name: "VacationRequests");

            migrationBuilder.DropTable(
                name: "WorkerPayrolls");

            migrationBuilder.DropTable(
                name: "WorkGroupWorker");

            migrationBuilder.DropTable(
                name: "WorkGroupWorkShift");

            migrationBuilder.DropTable(
                name: "WorkGroupWorkTask");

            migrationBuilder.DropTable(
                name: "Calendars");

            migrationBuilder.DropTable(
                name: "Places");

            migrationBuilder.DropTable(
                name: "Signings");

            migrationBuilder.DropTable(
                name: "Business");

            migrationBuilder.DropTable(
                name: "WorkShifts");

            migrationBuilder.DropTable(
                name: "WorkGroups");

            migrationBuilder.DropTable(
                name: "WorkTasks");

            migrationBuilder.DropTable(
                name: "Workers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.CreateTable(
                name: "Empresa",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CIF = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CodigoCuentaMonetaria = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Direccion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
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
                name: "SolicitudesVacaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    SeAcepta = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Trabajador = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    Descripcion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NombreTarea = table.Column<string>(type: "longtext", nullable: false)
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
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Eliminado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsDomingo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsJueves = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsLunes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsMartes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsMiercoles = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsSabado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    EsViernes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    HoraEntrada = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HoraSalida = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValidoDesde = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ValidoHasta = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Username = table.Column<string>(type: "longtext", nullable: false)
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
                    usuarioIdUser = table.Column<int>(type: "int", nullable: false),
                    nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    perteneceaturnos = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                name: "Fichajes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    Entrada_Salida = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FechaFichaje = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fichajes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fichajes_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    FechaIncidencia = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Justificada = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MotivoIncidencia = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Trabajador_Trabajadornumero_tarjeta",
                        column: x => x.Trabajadornumero_tarjeta,
                        principalTable: "Trabajador",
                        principalColumn: "numero_tarjeta",
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
                    Trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false),
                    HorasEspeciales = table.Column<int>(type: "int", nullable: false),
                    HorasNormales = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_NominasTrabajadores_Trabajador_Trabajadornumero_tarjeta",
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
                    EnHora = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FinTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HorasUsadas = table.Column<double>(type: "double", nullable: false),
                    inicioTarea = table.Column<DateTime>(type: "datetime(6)", nullable: false)
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
                name: "DiaLibre",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CalendarioPerteneceId = table.Column<int>(type: "int", nullable: false),
                    Disfrutado = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Motivo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                    fichajeId = table.Column<int>(type: "int", nullable: false),
                    trabajadornumero_tarjeta = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_DiaLibre_CalendarioPerteneceId",
                table: "DiaLibre",
                column: "CalendarioPerteneceId");

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
                name: "IX_Fichajes_Trabajadornumero_tarjeta",
                table: "Fichajes",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_Trabajadornumero_tarjeta",
                table: "Incidencias",
                column: "Trabajadornumero_tarjeta");

            migrationBuilder.CreateIndex(
                name: "IX_NominasTrabajadores_EmpresaId",
                table: "NominasTrabajadores",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_NominasTrabajadores_Trabajadornumero_tarjeta",
                table: "NominasTrabajadores",
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
    }
}
