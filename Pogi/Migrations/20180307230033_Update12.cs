using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestName = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PlayDate = table.Column<DateTime>(nullable: false),
                    preferTeeTimeId1 = table.Column<int>(nullable: false),
                    preferTeeTimeId2 = table.Column<int>(nullable: false),
                    preferTeeTimeId3 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestName = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    PlayDate = table.Column<DateTime>(nullable: false),
                    preferTeeTimeId1 = table.Column<int>(nullable: false),
                    preferTeeTimeId2 = table.Column<int>(nullable: false),
                    preferTeeTimeId3 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayId);
                });
        }
    }
}
