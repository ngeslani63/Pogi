using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update41 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TourEvent",
                table: "Score",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "TourId",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourScore",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Tour",
                columns: table => new
                {
                    TourId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CourseName = table.Column<string>(maxLength: 80, nullable: false),
                    HcpAllowPct = table.Column<float>(nullable: false),
                    TourDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tour", x => x.TourId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tour");

            migrationBuilder.DropColumn(
                name: "TourEvent",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "TourId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "TourScore",
                table: "Score");
        }
    }
}
