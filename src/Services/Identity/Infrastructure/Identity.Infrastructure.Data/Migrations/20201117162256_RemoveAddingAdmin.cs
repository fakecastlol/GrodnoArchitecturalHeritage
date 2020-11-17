using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Infrastructure.Data.Migrations
{
    public partial class RemoveAddingAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("444c6a18-24be-47cc-938b-0a62afc14256"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Password", "Role" },
                values: new object[] { new Guid("444c6a18-24be-47cc-938b-0a62afc14256"), "admin@gmail.ru", "admin", 1 });
        }
    }
}
