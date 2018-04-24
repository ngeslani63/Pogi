using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update36 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Disaster",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxScore",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuadBogeys",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripleBogeys",
                table: "Score",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Disaster",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "MaxScore",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "QuadBogeys",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "TripleBogeys",
                table: "Score");
        }
    }
}
