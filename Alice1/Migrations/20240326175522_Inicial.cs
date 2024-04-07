using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alice1.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "developers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_developers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "skills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    developerId = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    hook_url = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_skills_developers_developerId",
                        column: x => x.developerId,
                        principalTable: "developers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReqRess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    skillId = table.Column<int>(type: "int", nullable: false),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReqRess", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReqRess_skills_skillId",
                        column: x => x.skillId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    request = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    skillId = table.Column<int>(type: "int", nullable: false),
                    date = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_users_skills_skillId",
                        column: x => x.skillId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReqRess_skillId",
                table: "ReqRess",
                column: "skillId");

            migrationBuilder.CreateIndex(
                name: "IX_skills_developerId",
                table: "skills",
                column: "developerId");

            migrationBuilder.CreateIndex(
                name: "IX_users_skillId",
                table: "users",
                column: "skillId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReqRess");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "skills");

            migrationBuilder.DropTable(
                name: "developers");
        }
    }
}
