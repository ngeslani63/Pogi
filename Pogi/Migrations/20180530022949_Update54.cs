using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update54 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseHandicap",
                columns: table => new
                {
                    CourseHandicapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    LadiesHcp01 = table.Column<int>(nullable: false),
                    LadiesHcp02 = table.Column<int>(nullable: false),
                    LadiesHcp03 = table.Column<int>(nullable: false),
                    LadiesHcp04 = table.Column<int>(nullable: false),
                    LadiesHcp05 = table.Column<int>(nullable: false),
                    LadiesHcp06 = table.Column<int>(nullable: false),
                    LadiesHcp07 = table.Column<int>(nullable: false),
                    LadiesHcp08 = table.Column<int>(nullable: false),
                    LadiesHcp09 = table.Column<int>(nullable: false),
                    LadiesHcp10 = table.Column<int>(nullable: false),
                    LadiesHcp11 = table.Column<int>(nullable: false),
                    LadiesHcp12 = table.Column<int>(nullable: false),
                    LadiesHcp13 = table.Column<int>(nullable: false),
                    LadiesHcp14 = table.Column<int>(nullable: false),
                    LadiesHcp15 = table.Column<int>(nullable: false),
                    LadiesHcp16 = table.Column<int>(nullable: false),
                    LadiesHcp17 = table.Column<int>(nullable: false),
                    LadiesHcp18 = table.Column<int>(nullable: false),
                    MenHcp01 = table.Column<int>(nullable: false),
                    MenHcp02 = table.Column<int>(nullable: false),
                    MenHcp03 = table.Column<int>(nullable: false),
                    MenHcp04 = table.Column<int>(nullable: false),
                    MenHcp05 = table.Column<int>(nullable: false),
                    MenHcp06 = table.Column<int>(nullable: false),
                    MenHcp07 = table.Column<int>(nullable: false),
                    MenHcp08 = table.Column<int>(nullable: false),
                    MenHcp09 = table.Column<int>(nullable: false),
                    MenHcp10 = table.Column<int>(nullable: false),
                    MenHcp11 = table.Column<int>(nullable: false),
                    MenHcp12 = table.Column<int>(nullable: false),
                    MenHcp13 = table.Column<int>(nullable: false),
                    MenHcp14 = table.Column<int>(nullable: false),
                    MenHcp15 = table.Column<int>(nullable: false),
                    MenHcp16 = table.Column<int>(nullable: false),
                    MenHcp17 = table.Column<int>(nullable: false),
                    MenHcp18 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseHandicap", x => x.CourseHandicapId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Member_GhinNumber",
                table: "Member",
                column: "GhinNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CourseHandicap_CourseId",
                table: "CourseHandicap",
                column: "CourseId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseHandicap");

            migrationBuilder.DropIndex(
                name: "IX_Member_GhinNumber",
                table: "Member");
        }
    }
}
