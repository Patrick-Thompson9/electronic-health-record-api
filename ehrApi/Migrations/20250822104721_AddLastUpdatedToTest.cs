using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ehrApi.Migrations
{
    /// <inheritdoc />
    public partial class AddLastUpdatedToTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Tests",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TestType",
                table: "Tests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Tests");

            migrationBuilder.DropColumn(
                name: "TestType",
                table: "Tests");
        }
    }
}
