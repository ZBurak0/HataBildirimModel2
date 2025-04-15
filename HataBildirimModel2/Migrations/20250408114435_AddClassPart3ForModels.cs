using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HataBildirimModel2.Migrations
{
    /// <inheritdoc />
    public partial class AddClassPart3ForModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FaulTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaulTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FaultNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FaulTypeId = table.Column<int>(type: "int", nullable: false),
                    Explanation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaultNotifications_FaulTypes_FaulTypeId",
                        column: x => x.FaulTypeId,
                        principalTable: "FaulTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultNotifications_Filens_FileId",
                        column: x => x.FileId,
                        principalTable: "Filens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultNotifications_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FaultNotifications_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FaultNotifications_FaulTypeId",
                table: "FaultNotifications",
                column: "FaulTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultNotifications_FileId",
                table: "FaultNotifications",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultNotifications_LocationId",
                table: "FaultNotifications",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultNotifications_UserId",
                table: "FaultNotifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FaultNotifications");

            migrationBuilder.DropTable(
                name: "FaulTypes");

            migrationBuilder.DropTable(
                name: "Filens");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
