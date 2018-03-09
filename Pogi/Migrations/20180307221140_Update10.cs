using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    PlayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestName = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Playdate = table.Column<DateTime>(nullable: false),
                    preferTeeTimeId1 = table.Column<int>(nullable: false),
                    preferTeeTimeId2 = table.Column<int>(nullable: false),
                    preferTeeTimeId3 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.PlayId);
                });

            migrationBuilder.CreateTable(
                name: "Score",
                columns: table => new
                {
                    ScoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AttestedTS = table.Column<DateTime>(nullable: false),
                    Color = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false),
                    EnteredById = table.Column<int>(nullable: false),
                    Hole01 = table.Column<int>(nullable: false),
                    Hole02 = table.Column<int>(nullable: false),
                    Hole03 = table.Column<int>(nullable: false),
                    Hole04 = table.Column<int>(nullable: false),
                    Hole05 = table.Column<int>(nullable: false),
                    Hole06 = table.Column<int>(nullable: false),
                    Hole07 = table.Column<int>(nullable: false),
                    Hole08 = table.Column<int>(nullable: false),
                    Hole09 = table.Column<int>(nullable: false),
                    Hole10 = table.Column<int>(nullable: false),
                    Hole11 = table.Column<int>(nullable: false),
                    Hole12 = table.Column<int>(nullable: false),
                    Hole13 = table.Column<int>(nullable: false),
                    Hole14 = table.Column<int>(nullable: false),
                    Hole15 = table.Column<int>(nullable: false),
                    Hole16 = table.Column<int>(nullable: false),
                    Hole17 = table.Column<int>(nullable: false),
                    Hole18 = table.Column<int>(nullable: false),
                    HoleIn = table.Column<int>(nullable: false),
                    HoleOut = table.Column<int>(nullable: false),
                    HoleTotal = table.Column<int>(nullable: false),
                    MemberId = table.Column<int>(nullable: false),
                    ScoreTS = table.Column<DateTime>(nullable: false),
                    TeeTimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Score", x => x.ScoreId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Score");
        }
    }
}
