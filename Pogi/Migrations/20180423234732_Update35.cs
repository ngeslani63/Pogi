using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update35 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutGame",
                table: "Score",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Albatross",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Birdies",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Bogeys",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoubleBogeys",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Eagles",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HoleInOnes",
                table: "Score",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pars",
                table: "Score",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutGame",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Albatross",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Birdies",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Bogeys",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "DoubleBogeys",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Eagles",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "HoleInOnes",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "Pars",
                table: "Score");
        }
    }
}
