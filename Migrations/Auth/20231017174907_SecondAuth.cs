﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kursach.Migrations.Auth
{
    /// <inheritdoc />
    public partial class SecondAuth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "type",
                table: "Auths",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "Auths");
        }
    }
}
