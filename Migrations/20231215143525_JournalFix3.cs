using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class JournalFix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                columns: new[] { "studentID", "groupID", "lessonID" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                column: "studentID");
        }
    }
}
