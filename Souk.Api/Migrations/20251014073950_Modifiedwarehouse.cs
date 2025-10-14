using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Souk.Api.Migrations
{
    /// <inheritdoc />
    public partial class Modifiedwarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CapacityUsed",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CapacityUsed",
                table: "Warehouses");
        }
    }
}
