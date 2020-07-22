using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update62 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseMap",
                columns: table => new
                {
                    CourseMapId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseId = table.Column<int>(nullable: false),
                    CourseLat = table.Column<double>(nullable: false),
                    CourseLng = table.Column<double>(nullable: false),
                    Lat01 = table.Column<double>(nullable: false),
                    Lat02 = table.Column<double>(nullable: false),
                    Lat03 = table.Column<double>(nullable: false),
                    Lat04 = table.Column<double>(nullable: false),
                    Lat05 = table.Column<double>(nullable: false),
                    Lat06 = table.Column<double>(nullable: false),
                    Lat07 = table.Column<double>(nullable: false),
                    Lat08 = table.Column<double>(nullable: false),
                    Lat09 = table.Column<double>(nullable: false),
                    Lat10 = table.Column<double>(nullable: false),
                    Lat11 = table.Column<double>(nullable: false),
                    Lat12 = table.Column<double>(nullable: false),
                    Lat13 = table.Column<double>(nullable: false),
                    Lat14 = table.Column<double>(nullable: false),
                    Lat15 = table.Column<double>(nullable: false),
                    Lat16 = table.Column<double>(nullable: false),
                    Lat17 = table.Column<double>(nullable: false),
                    Lat18 = table.Column<double>(nullable: false),
                    Lng01 = table.Column<double>(nullable: false),
                    Lng02 = table.Column<double>(nullable: false),
                    Lng03 = table.Column<double>(nullable: false),
                    Lng04 = table.Column<double>(nullable: false),
                    Lng05 = table.Column<double>(nullable: false),
                    Lng06 = table.Column<double>(nullable: false),
                    Lng07 = table.Column<double>(nullable: false),
                    Lng08 = table.Column<double>(nullable: false),
                    Lng09 = table.Column<double>(nullable: false),
                    Lng10 = table.Column<double>(nullable: false),
                    Lng11 = table.Column<double>(nullable: false),
                    Lng12 = table.Column<double>(nullable: false),
                    Lng13 = table.Column<double>(nullable: false),
                    Lng14 = table.Column<double>(nullable: false),
                    Lng15 = table.Column<double>(nullable: false),
                    Lng16 = table.Column<double>(nullable: false),
                    Lng17 = table.Column<double>(nullable: false),
                    Lng18 = table.Column<double>(nullable: false),
                    Zoom = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseMap", x => x.CourseMapId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseMap");
        }
    }
}
