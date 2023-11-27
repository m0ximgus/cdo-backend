using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class Global : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authorizations",
                columns: table => new
                {
                    authToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    type = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authorizations", x => x.authToken);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    groupID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    groupName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.groupID);
                });

            migrationBuilder.CreateTable(
                name: "JobTitles",
                columns: table => new
                {
                    jobID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    jobName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    salary = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobTitles", x => x.jobID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    subjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subjectName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.subjectID);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    studentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullNameStudent = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    age = table.Column<DateTime>(type: "datetime2", nullable: false),
                    enrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    contactMailStudent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhoneStudent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<bool>(type: "bit", nullable: false),
                    groupID = table.Column<int>(type: "int", nullable: true),
                    authToken = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.studentID);
                    table.ForeignKey(
                        name: "FK_Students_Authorizations_authToken",
                        column: x => x.authToken,
                        principalTable: "Authorizations",
                        principalColumn: "authToken");
                    table.ForeignKey(
                        name: "FK_Students_Groups_groupID",
                        column: x => x.groupID,
                        principalTable: "Groups",
                        principalColumn: "groupID");
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullNameEmployee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactMailEmployee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhoneEmployee = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authToken = table.Column<int>(type: "int", nullable: true),
                    jobID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employeeID);
                    table.ForeignKey(
                        name: "FK_Employees_Authorizations_authToken",
                        column: x => x.authToken,
                        principalTable: "Authorizations",
                        principalColumn: "authToken");
                    table.ForeignKey(
                        name: "FK_Employees_JobTitles_jobID",
                        column: x => x.jobID,
                        principalTable: "JobTitles",
                        principalColumn: "jobID");
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    teacherID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fullNameTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactMailTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    contactPhoneTeacher = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    authToken = table.Column<int>(type: "int", nullable: true),
                    jobID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.teacherID);
                    table.ForeignKey(
                        name: "FK_Teachers_Authorizations_authToken",
                        column: x => x.authToken,
                        principalTable: "Authorizations",
                        principalColumn: "authToken");
                    table.ForeignKey(
                        name: "FK_Teachers_JobTitles_jobID",
                        column: x => x.jobID,
                        principalTable: "JobTitles",
                        principalColumn: "jobID");
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    studentID = table.Column<int>(type: "int", nullable: false),
                    paymentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    paymentCost = table.Column<double>(type: "float", nullable: false),
                    paymentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.studentID);
                    table.ForeignKey(
                        name: "FK_Payments_Students_studentID",
                        column: x => x.studentID,
                        principalTable: "Students",
                        principalColumn: "studentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    groupID = table.Column<int>(type: "int", nullable: false),
                    teacherID = table.Column<int>(type: "int", nullable: false),
                    subjectID = table.Column<int>(type: "int", nullable: false),
                    classroom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => new { x.groupID, x.teacherID, x.subjectID });
                    table.ForeignKey(
                        name: "FK_Lessons_Groups_groupID",
                        column: x => x.groupID,
                        principalTable: "Groups",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Subjects_subjectID",
                        column: x => x.subjectID,
                        principalTable: "Subjects",
                        principalColumn: "subjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lessons_Teachers_teacherID",
                        column: x => x.teacherID,
                        principalTable: "Teachers",
                        principalColumn: "teacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_authToken",
                table: "Employees",
                column: "authToken");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_jobID",
                table: "Employees",
                column: "jobID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_subjectID",
                table: "Lessons",
                column: "subjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_teacherID",
                table: "Lessons",
                column: "teacherID");

            migrationBuilder.CreateIndex(
                name: "IX_Students_authToken",
                table: "Students",
                column: "authToken");

            migrationBuilder.CreateIndex(
                name: "IX_Students_groupID",
                table: "Students",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_authToken",
                table: "Teachers",
                column: "authToken");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_jobID",
                table: "Teachers",
                column: "jobID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "JobTitles");

            migrationBuilder.DropTable(
                name: "Authorizations");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
