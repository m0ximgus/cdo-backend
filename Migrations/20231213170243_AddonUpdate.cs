using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class AddonUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Addons_lessonID",
                table: "Addons",
                column: "lessonID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addons_Lessons_lessonID",
                table: "Addons",
                column: "lessonID",
                principalTable: "Lessons",
                principalColumn: "lessonID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addons_Lessons_lessonID",
                table: "Addons");

            migrationBuilder.DropIndex(
                name: "IX_Addons_lessonID",
                table: "Addons");
        }
    }
}
