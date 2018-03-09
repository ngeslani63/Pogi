using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CourseName = table.Column<string>(maxLength: 80, nullable: false),
                    Par01 = table.Column<int>(nullable: false),
                    Par02 = table.Column<int>(nullable: false),
                    Par03 = table.Column<int>(nullable: false),
                    Par04 = table.Column<int>(nullable: false),
                    Par05 = table.Column<int>(nullable: false),
                    Par06 = table.Column<int>(nullable: false),
                    Par07 = table.Column<int>(nullable: false),
                    Par08 = table.Column<int>(nullable: false),
                    Par09 = table.Column<int>(nullable: false),
                    Par10 = table.Column<int>(nullable: false),
                    Par11 = table.Column<int>(nullable: false),
                    Par12 = table.Column<int>(nullable: false),
                    Par13 = table.Column<int>(nullable: false),
                    Par14 = table.Column<int>(nullable: false),
                    Par15 = table.Column<int>(nullable: false),
                    Par16 = table.Column<int>(nullable: false),
                    Par17 = table.Column<int>(nullable: false),
                    Par18 = table.Column<int>(nullable: false),
                    ParIn = table.Column<int>(nullable: false),
                    ParOut = table.Column<int>(nullable: false),
                    ParTotal = table.Column<int>(nullable: false),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "CourseDetail",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false),
                    Color = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false),
                    Slope = table.Column<int>(nullable: false),
                    Yards01 = table.Column<int>(nullable: false),
                    Yards02 = table.Column<int>(nullable: false),
                    Yards03 = table.Column<int>(nullable: false),
                    Yards04 = table.Column<int>(nullable: false),
                    Yards05 = table.Column<int>(nullable: false),
                    Yards06 = table.Column<int>(nullable: false),
                    Yards07 = table.Column<int>(nullable: false),
                    Yards08 = table.Column<int>(nullable: false),
                    Yards09 = table.Column<int>(nullable: false),
                    Yards10 = table.Column<int>(nullable: false),
                    Yards11 = table.Column<int>(nullable: false),
                    Yards12 = table.Column<int>(nullable: false),
                    Yards13 = table.Column<int>(nullable: false),
                    Yards14 = table.Column<int>(nullable: false),
                    Yards15 = table.Column<int>(nullable: false),
                    Yards16 = table.Column<int>(nullable: false),
                    Yards17 = table.Column<int>(nullable: false),
                    Yards18 = table.Column<int>(nullable: false),
                    YardsIn = table.Column<int>(nullable: false),
                    YardsOut = table.Column<int>(nullable: false),
                    YardsTotal = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseDetail", x => new { x.CourseId, x.Color });
                    table.UniqueConstraint("AK_CourseDetail_Color_CourseId", x => new { x.Color, x.CourseId });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "CourseDetail");
        }
    }
}
