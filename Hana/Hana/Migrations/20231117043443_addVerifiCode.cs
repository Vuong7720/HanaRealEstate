using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hana.Migrations
{
    /// <inheritdoc />
    public partial class addVerifiCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__REAL_EST__C0378634981FED9E",
                table: "REAL_ESTATE_DETAIL");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "REAL_ESTATE_DETAIL",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationCode",
                table: "AGENT",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "UQ__REAL_EST__C0378634981FED9E",
                table: "REAL_ESTATE_DETAIL",
                column: "RealEstateId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__REAL_EST__C0378634981FED9E",
                table: "REAL_ESTATE_DETAIL");

            migrationBuilder.DropColumn(
                name: "VerificationCode",
                table: "AGENT");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "REAL_ESTATE_DETAIL",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "UQ__REAL_EST__C0378634981FED9E",
                table: "REAL_ESTATE_DETAIL",
                column: "RealEstateId",
                unique: true,
                filter: "[RealEstateId] IS NOT NULL");
        }
    }
}
