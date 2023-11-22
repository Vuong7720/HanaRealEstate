using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hana.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ABOUT_US",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "ntext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ABOUT_US", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CITY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CITY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "ntext", nullable: true),
                    Answer = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LEVEL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LEVEL", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "POLICY",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyContent = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_POLICY", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "REAL_ESTATE_TYPE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REAL_ESTATE_TYPE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DISTRICT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DistrictName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DISTRICT", x => x.Id);
                    table.ForeignKey(
                        name: "FK__DISTRICT__CityId__145C0A3F",
                        column: x => x.CityId,
                        principalTable: "CITY",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AGENT",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    Facebook = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Zalo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LoginName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ActiveKey = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    ResetPasswordKey = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: true),
                    LastLogin = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    ConfirmPhoneNumber = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AGENT", x => x.Id);
                    table.ForeignKey(
                        name: "FK__AGENT__LevelId__3B75D760",
                        column: x => x.LevelId,
                        principalTable: "LEVEL",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WARD",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WardName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DistrictId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARD", x => x.Id);
                    table.ForeignKey(
                        name: "FK__WARD__DistrictId__173876EA",
                        column: x => x.DistrictId,
                        principalTable: "DISTRICT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REAL_ESTATE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PostTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime", nullable: true),
                    BeginTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExprireTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RealEstateTypeId = table.Column<int>(type: "int", nullable: true),
                    AgentId = table.Column<int>(type: "int", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsConfirm = table.Column<bool>(type: "bit", nullable: false),
                    ConfirmStatus = table.Column<int>(type: "int", nullable: false),
                    IsAvaiable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REAL_ESTATE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__REAL_ESTA__Agent__3F466844",
                        column: x => x.AgentId,
                        principalTable: "AGENT",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__REAL_ESTA__ReaEs__3E52440B",
                        column: x => x.RealEstateTypeId,
                        principalTable: "REAL_ESTATE_TYPE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SOCIAL_LOGIN",
                columns: table => new
                {
                    ProviderKey = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Provider = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__SOCIAL_L__8DE43C5E9E312291", x => x.ProviderKey);
                    table.ForeignKey(
                        name: "FK__SOCIAL_LO__UserI__52593CB8",
                        column: x => x.UserId,
                        principalTable: "AGENT",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MAP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Latitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    Longtitude = table.Column<decimal>(type: "decimal(9,6)", nullable: true),
                    WardId = table.Column<int>(type: "int", nullable: true),
                    DistrictId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    RealEstateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAP", x => x.Id);
                    table.ForeignKey(
                        name: "FK__MAP__CityId__48CFD27E",
                        column: x => x.CityId,
                        principalTable: "CITY",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__MAP__DistrictId__47DBAE45",
                        column: x => x.DistrictId,
                        principalTable: "DISTRICT",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK__MAP__RealEstateI__49C3F6B7",
                        column: x => x.RealEstateId,
                        principalTable: "REAL_ESTATE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__MAP__WardId__46E78A0C",
                        column: x => x.WardId,
                        principalTable: "WARD",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PICTURE",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PictureName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NativeWidth = table.Column<int>(type: "int", nullable: false),
                    NativeHeight = table.Column<int>(type: "int", nullable: false),
                    RealEstateId = table.Column<int>(type: "int", nullable: true),
                    URL = table.Column<string>(type: "varchar(300)", unicode: false, maxLength: 300, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PICTURE", x => x.Id);
                    table.ForeignKey(
                        name: "FK__PICTURE__RealEst__4CA06362",
                        column: x => x.RealEstateId,
                        principalTable: "REAL_ESTATE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RATING",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    RealEstateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RATING", x => x.Id);
                    table.ForeignKey(
                        name: "FK__RATING__RealEsta__4F7CD00D",
                        column: x => x.RealEstateId,
                        principalTable: "REAL_ESTATE",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "REAL_ESTATE_DETAIL",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Acreage = table.Column<int>(type: "int", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    HasPrivateWC = table.Column<bool>(type: "bit", nullable: false),
                    HasMezzanine = table.Column<bool>(type: "bit", nullable: false),
                    AllowCook = table.Column<bool>(type: "bit", nullable: false),
                    FreeTime = table.Column<bool>(type: "bit", nullable: false),
                    SecurityCamera = table.Column<bool>(type: "bit", nullable: false),
                    WaterPrice = table.Column<int>(type: "int", nullable: true),
                    ElectronicPrice = table.Column<int>(type: "int", nullable: true),
                    WifiPrice = table.Column<decimal>(type: "decimal(18,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REAL_ESTATE_DETAIL", x => x.Id);
                    table.ForeignKey(
                        name: "FK__REAL_ESTA__RealE__5629CD9C",
                        column: x => x.RealEstateId,
                        principalTable: "REAL_ESTATE",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AGENT_LevelId",
                table: "AGENT",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_DISTRICT_CityId",
                table: "DISTRICT",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MAP_CityId",
                table: "MAP",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_MAP_DistrictId",
                table: "MAP",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_MAP_WardId",
                table: "MAP",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "UQ__MAP__C037863418DC284F",
                table: "MAP",
                column: "RealEstateId",
                unique: true,
                filter: "[RealEstateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PICTURE_RealEstateId",
                table: "PICTURE",
                column: "RealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_RATING_RealEstateId",
                table: "RATING",
                column: "RealEstateId");

            migrationBuilder.CreateIndex(
                name: "IX_REAL_ESTATE_AgentId",
                table: "REAL_ESTATE",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_REAL_ESTATE_RealEstateTypeId",
                table: "REAL_ESTATE",
                column: "RealEstateTypeId");

            migrationBuilder.CreateIndex(
                name: "UQ__REAL_EST__C0378634981FED9E",
                table: "REAL_ESTATE_DETAIL",
                column: "RealEstateId",
                unique: true,
                filter: "[RealEstateId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SOCIAL_LOGIN_UserId",
                table: "SOCIAL_LOGIN",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WARD_DistrictId",
                table: "WARD",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ABOUT_US");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "MAP");

            migrationBuilder.DropTable(
                name: "PICTURE");

            migrationBuilder.DropTable(
                name: "POLICY");

            migrationBuilder.DropTable(
                name: "RATING");

            migrationBuilder.DropTable(
                name: "REAL_ESTATE_DETAIL");

            migrationBuilder.DropTable(
                name: "SOCIAL_LOGIN");

            migrationBuilder.DropTable(
                name: "WARD");

            migrationBuilder.DropTable(
                name: "REAL_ESTATE");

            migrationBuilder.DropTable(
                name: "DISTRICT");

            migrationBuilder.DropTable(
                name: "AGENT");

            migrationBuilder.DropTable(
                name: "REAL_ESTATE_TYPE");

            migrationBuilder.DropTable(
                name: "CITY");

            migrationBuilder.DropTable(
                name: "LEVEL");
        }
    }
}
