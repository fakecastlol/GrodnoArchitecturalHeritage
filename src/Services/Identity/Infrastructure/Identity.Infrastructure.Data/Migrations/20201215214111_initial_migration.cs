using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Data.Migrations
{
    public partial class initial_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(maxLength: 30, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 20, nullable: true),
                    LastName = table.Column<string>(maxLength: 20, nullable: true),
                    Login = table.Column<string>(maxLength: 20, nullable: true),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    LastVisited = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    Location = table.Column<string>(maxLength: 20, nullable: true),
                    ArticleCount = table.Column<int>(nullable: false),
                    MessagesCount = table.Column<int>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Avatar = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
