using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BaseApplication.Repository.Migrations
{
    /// <inheritdoc />
    public partial class TablesForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Screens",
                columns: table => new
                {
                    ScreenId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Screens", x => x.ScreenId);
                });

            migrationBuilder.CreateTable(
                name: "ScreenActions",
                columns: table => new
                {
                    ScreenActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenActionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScreenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenActions", x => x.ScreenActionId);
                    table.ForeignKey(
                        name: "FK_ScreenActions_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "ScreenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScreenRoles",
                columns: table => new
                {
                    ScreenRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScreenRoles", x => x.ScreenRoleId);
                    table.ForeignKey(
                        name: "FK_ScreenRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ScreenRoles_Screens_ScreenId",
                        column: x => x.ScreenId,
                        principalTable: "Screens",
                        principalColumn: "ScreenId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleScreenActions",
                columns: table => new
                {
                    RoleScreenActionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScreenRoleId = table.Column<int>(type: "int", nullable: false),
                    ScreenActionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleScreenActions", x => x.RoleScreenActionId);
                    table.ForeignKey(
                        name: "FK_RoleScreenActions_ScreenActions_ScreenActionId",
                        column: x => x.ScreenActionId,
                        principalTable: "ScreenActions",
                        principalColumn: "ScreenActionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleScreenActions_ScreenRoles_ScreenRoleId",
                        column: x => x.ScreenRoleId,
                        principalTable: "ScreenRoles",
                        principalColumn: "ScreenRoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenActions_ScreenActionId",
                table: "RoleScreenActions",
                column: "ScreenActionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleScreenActions_ScreenRoleId",
                table: "RoleScreenActions",
                column: "ScreenRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenActions_ScreenId",
                table: "ScreenActions",
                column: "ScreenId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenRoles_RoleId",
                table: "ScreenRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScreenRoles_ScreenId",
                table: "ScreenRoles",
                column: "ScreenId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleScreenActions");

            migrationBuilder.DropTable(
                name: "ScreenActions");

            migrationBuilder.DropTable(
                name: "ScreenRoles");

            migrationBuilder.DropTable(
                name: "Screens");
        }
    }
}
