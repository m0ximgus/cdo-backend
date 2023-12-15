using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class JouranlFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_studentID",
                table: "Journal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                column: "studentID");

            migrationBuilder.CreateIndex(
                name: "IX_Journal_groupID",
                table: "Journal",
                column: "groupID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Journal",
                table: "Journal");

            migrationBuilder.DropIndex(
                name: "IX_Journal_groupID",
                table: "Journal");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Journal",
                table: "Journal",
                columns: new[] { "groupID", "studentID" });

            migrationBuilder.CreateIndex(
                name: "IX_Journal_studentID",
                table: "Journal",
                column: "studentID");
        }
    }
}
