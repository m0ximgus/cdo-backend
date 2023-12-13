using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations
{
    /// <inheritdoc />
    public partial class Addon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addons",
                columns: table => new
                {
                    addonID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    addonHeader = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    addonDescription = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    lessonID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addons", x => x.addonID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addons");
        }
    }
}
