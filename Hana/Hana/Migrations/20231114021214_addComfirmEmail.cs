using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hana.Migrations
{
    /// <inheritdoc />
    public partial class addComfirmEmail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__PICTURE__RealEst__4CA06362",
                table: "PICTURE");

            migrationBuilder.AddColumn<bool>(
                name: "ConfirmEmail",
                table: "AGENT",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK__PICTURE__RealEst__4CA06362",
                table: "PICTURE",
                column: "RealEstateId",
                principalTable: "REAL_ESTATE",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__PICTURE__RealEst__4CA06362",
                table: "PICTURE");

            migrationBuilder.DropColumn(
                name: "ConfirmEmail",
                table: "AGENT");

            migrationBuilder.AddForeignKey(
                name: "FK__PICTURE__RealEst__4CA06362",
                table: "PICTURE",
                column: "RealEstateId",
                principalTable: "REAL_ESTATE",
                principalColumn: "Id");
        }
    }
}
