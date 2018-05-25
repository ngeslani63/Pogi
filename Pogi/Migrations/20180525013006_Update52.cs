using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update52 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "TeeTime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "color",
                table: "TeeAssign",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "IpGuest",
                columns: table => new
                {
                    IpAddr = table.Column<string>(nullable: false),
                    LastUpdtTs = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IpGuest", x => x.IpAddr);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IpGuest");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "TeeTime");

            migrationBuilder.DropColumn(
                name: "color",
                table: "TeeAssign");
        }
    }
}
