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
                    BelongstoWorkGroups = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserIdUser = table.Column<int>(type: "int", nullable: false),
                    Category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HiringDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Nif = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SocialSecurityCard = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                name: "Logs");

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
        }
    }
}
