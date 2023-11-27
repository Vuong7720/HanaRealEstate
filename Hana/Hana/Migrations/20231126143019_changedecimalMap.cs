using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hana.Migrations
{
    /// <inheritdoc />
    public partial class changedecimalMap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longtitude",
                table: "MAP",
                type: "decimal(10,7)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "MAP",
                type: "decimal(9,7)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,0)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Longtitude",
                table: "MAP",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,7)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "MAP",
                type: "decimal(10,0)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,7)",
                oldNullable: true);
        }
    }
}
