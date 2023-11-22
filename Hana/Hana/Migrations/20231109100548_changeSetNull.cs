using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hana.Migrations
{
    /// <inheritdoc />
    public partial class changeSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__MAP__CityId__48CFD27E",
                table: "MAP");

            migrationBuilder.DropForeignKey(
                name: "FK__MAP__DistrictId__47DBAE45",
                table: "MAP");

            migrationBuilder.DropForeignKey(
                name: "FK__MAP__WardId__46E78A0C",
                table: "MAP");

            migrationBuilder.DropIndex(
                name: "UQ__MAP__C037863418DC284F",
                table: "MAP");

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "MAP",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "MAP",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Longtitude",
                table: "MAP",
                type: "decimal(38,15)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "MAP",
                type: "decimal(38,15)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(9,6)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "MAP",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "MAP",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "UQ__MAP__C037863418DC284F",
                table: "MAP",
                column: "RealEstateId",
                unique: true,
                filter: "[RealEstateId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__CityId__48CFD27E",
                table: "MAP",
                column: "CityId",
                principalTable: "CITY",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__DistrictId__47DBAE45",
                table: "MAP",
                column: "DistrictId",
                principalTable: "DISTRICT",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__WardId__46E78A0C",
                table: "MAP",
                column: "WardId",
                principalTable: "WARD",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__MAP__CityId__48CFD27E",
                table: "MAP");

            migrationBuilder.DropForeignKey(
                name: "FK__MAP__DistrictId__47DBAE45",
                table: "MAP");

            migrationBuilder.DropForeignKey(
                name: "FK__MAP__WardId__46E78A0C",
                table: "MAP");

            migrationBuilder.DropIndex(
                name: "UQ__MAP__C037863418DC284F",
                table: "MAP");

            migrationBuilder.AlterColumn<int>(
                name: "WardId",
                table: "MAP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RealEstateId",
                table: "MAP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Longtitude",
                table: "MAP",
                type: "decimal(9,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Latitude",
                table: "MAP",
                type: "decimal(9,6)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,15)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DistrictId",
                table: "MAP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CityId",
                table: "MAP",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__MAP__C037863418DC284F",
                table: "MAP",
                column: "RealEstateId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__CityId__48CFD27E",
                table: "MAP",
                column: "CityId",
                principalTable: "CITY",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__DistrictId__47DBAE45",
                table: "MAP",
                column: "DistrictId",
                principalTable: "DISTRICT",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__MAP__WardId__46E78A0C",
                table: "MAP",
                column: "WardId",
                principalTable: "WARD",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
