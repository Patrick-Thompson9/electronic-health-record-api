using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ehrApi.Migrations
{
    /// <inheritdoc />
    public partial class RemovedTestType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TestType",
                table: "Tests");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TestType",
                table: "Tests",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
