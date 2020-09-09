using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update67 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MatchGrp",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MatchPlayerNum",
                table: "Score",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchGrp",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "MatchPlayerNum",
                table: "Score");
        }
    }
}
