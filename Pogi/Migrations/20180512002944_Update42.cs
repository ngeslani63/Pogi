using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update42 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseName",
                table: "Tour",
                newName: "TourName");

            migrationBuilder.AddColumn<int>(
                name: "ScorerType",
                table: "Tour",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourType",
                table: "Tour",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ScorerType",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "TourType",
                table: "Tour");

            migrationBuilder.RenameColumn(
                name: "TourName",
                table: "Tour",
                newName: "CourseName");
        }
    }
}
