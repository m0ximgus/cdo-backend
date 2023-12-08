using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class PreFinal3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Lessons_lessonID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal");

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
                name: "lessonID",
                table: "Journal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Journal_Lessons_lessonID",
                table: "Journal",
                column: "lessonID",
                principalTable: "Lessons",
                principalColumn: "lessonID",
                onDelete: ReferentialAction.Cascade);

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
                name: "FK_Journal_Lessons_lessonID",
                table: "Journal");

            migrationBuilder.DropForeignKey(
                name: "FK_Journal_Students_studentID",
                table: "Journal");

            migrationBuilder.AlterColumn<int>(
                name: "studentID",
                table: "Journal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "lessonID",
                table: "Journal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }
    }
}
