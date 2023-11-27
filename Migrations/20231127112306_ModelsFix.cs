﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class ModelsFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Teachers_teacherID",
                table: "Journal");

            migrationBuilder.RenameColumn(
                name: "teacherID",
                table: "Journal",
                newName: "studentID");

            migrationBuilder.RenameIndex(
                name: "IX_Journal_teacherID",
                table: "Journal",
                newName: "IX_Journal_studentID");

            migrationBuilder.CreateTable(
                name: "GroupSubject",
                columns: table => new
                {
                    GroupsgroupID = table.Column<int>(type: "int", nullable: false),
                    SubjectssubjectID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupSubject", x => new { x.GroupsgroupID, x.SubjectssubjectID });
                    table.ForeignKey(
                        name: "FK_GroupSubject_Groups_GroupsgroupID",
                        column: x => x.GroupsgroupID,
                        principalTable: "Groups",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupSubject_Subjects_SubjectssubjectID",
                        column: x => x.SubjectssubjectID,
                        principalTable: "Subjects",
                        principalColumn: "subjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupTeacher",
                columns: table => new
                {
                    GroupsgroupID = table.Column<int>(type: "int", nullable: false),
                    TeachersteacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupTeacher", x => new { x.GroupsgroupID, x.TeachersteacherID });
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Groups_GroupsgroupID",
                        column: x => x.GroupsgroupID,
                        principalTable: "Groups",
                        principalColumn: "groupID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupTeacher_Teachers_TeachersteacherID",
                        column: x => x.TeachersteacherID,
                        principalTable: "Teachers",
                        principalColumn: "teacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectTeacher",
                columns: table => new
                {
                    SubjectssubjectID = table.Column<int>(type: "int", nullable: false),
                    TeachersteacherID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectTeacher", x => new { x.SubjectssubjectID, x.TeachersteacherID });
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Subjects_SubjectssubjectID",
                        column: x => x.SubjectssubjectID,
                        principalTable: "Subjects",
                        principalColumn: "subjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubjectTeacher_Teachers_TeachersteacherID",
                        column: x => x.TeachersteacherID,
                        principalTable: "Teachers",
                        principalColumn: "teacherID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupSubject_SubjectssubjectID",
                table: "GroupSubject",
                column: "SubjectssubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_GroupTeacher_TeachersteacherID",
                table: "GroupTeacher",
                column: "TeachersteacherID");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectTeacher_TeachersteacherID",
                table: "SubjectTeacher",
                column: "TeachersteacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal",
                column: "studentID",
                principalTable: "Students",
                principalColumn: "studentID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal");

            migrationBuilder.DropTable(
                name: "GroupSubject");

            migrationBuilder.DropTable(
                name: "GroupTeacher");

            migrationBuilder.DropTable(
                name: "SubjectTeacher");

            migrationBuilder.RenameColumn(
                name: "studentID",
                table: "Journal",
                newName: "teacherID");

            migrationBuilder.RenameIndex(
                name: "IX_Journal_studentID",
                table: "Journal",
                newName: "IX_Journal_teacherID");

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Teachers_teacherID",
                table: "Journal",
                column: "teacherID",
                principalTable: "Teachers",
                principalColumn: "teacherID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
