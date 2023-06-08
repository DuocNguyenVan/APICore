using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SecurityAPI.Persistence.Migrations
{
    public partial class UpdateApplicationUserLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLogins", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserLogins");
        }
    }
}
