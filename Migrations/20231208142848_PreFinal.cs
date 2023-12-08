using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class PreFinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Groups_groupID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_subjectID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teachers_teacherID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_studentID",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.AlterColumn<int>(
                name: "studentID",
                table: "Payments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "paymentID",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "subjectID",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "teacherID",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "groupID",
                table: "Lessons",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "lessonID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "mark",
                table: "Journal",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "studentID",
                table: "Journal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "lessonID",
                table: "Journal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rating",
                table: "Journal",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "teacherID",
                table: "Journal",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "paymentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "lessonID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                column: "groupID");

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    eventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventHeader = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    eventDescription = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    eventDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.eventID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_studentID",
                table: "Payments",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_groupID",
                table: "Lessons",
                column: "groupID");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_lessonID",
                table: "Journal",
                column: "lessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_teacherID",
                table: "Journal",
                column: "teacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Lessons_lessonID",
                table: "Journal",
                column: "lessonID",
                principalTable: "Lessons",
                principalColumn: "lessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "studentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Teachers_teacherID",
                table: "Journal",
                column: "teacherID",
                principalTable: "Teachers",
                principalColumn: "teacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Groups_groupID",
                table: "Lessons",
                column: "groupID",
                principalTable: "Groups",
                principalColumn: "groupID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_subjectID",
                table: "Lessons",
                column: "subjectID",
                principalTable: "Subjects",
                principalColumn: "subjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teachers_teacherID",
                table: "Lessons",
                column: "teacherID",
                principalTable: "Teachers",
                principalColumn: "teacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_studentID",
                table: "Payments",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "studentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Lessons_lessonID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Teachers_teacherID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Groups_groupID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_subjectID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Teachers_teacherID",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Students_studentID",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Payments",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_studentID",
                table: "Payments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_groupID",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_lessonID",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_teacherID",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "paymentID",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "lessonID",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "lessonID",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "rating",
                table: "Journal");

            migrationBuilder.DropColumn(
                name: "teacherID",
                table: "Journal");

            migrationBuilder.AlterColumn<int>(
                name: "studentID",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "teacherID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "subjectID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "groupID",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "studentID",
                table: "Journal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "mark",
                table: "Journal",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Payments",
                table: "Payments",
                column: "studentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                columns: new[] { "groupID", "teacherID", "subjectID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                columns: new[] { "groupID", "studentID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Groups_groupID",
                table: "Lessons",
                column: "groupID",
                principalTable: "Groups",
                principalColumn: "groupID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_subjectID",
                table: "Lessons",
                column: "subjectID",
                principalTable: "Subjects",
                principalColumn: "subjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Teachers_teacherID",
                table: "Lessons",
                column: "teacherID",
                principalTable: "Teachers",
                principalColumn: "teacherID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Students_studentID",
                table: "Payments",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
