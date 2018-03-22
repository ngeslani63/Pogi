using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ScoreTS",
                table: "Score",
                newName: "ScoreDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Score",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTs",
                table: "Score",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Score",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedTs",
                table: "Score",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "CreatedTs",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTs",
                table: "Score");

            migrationBuilder.RenameColumn(
                name: "ScoreDate",
                table: "Score",
                newName: "ScoreTS");
        }
    }
}
