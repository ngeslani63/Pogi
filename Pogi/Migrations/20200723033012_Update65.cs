using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update65 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Zoom01",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom02",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom03",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom04",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom05",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom06",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom07",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom08",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom09",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom10",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom11",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom12",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom13",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom14",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom15",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom16",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom17",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Zoom18",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Zoom01",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom02",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom03",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom04",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom05",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom06",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom07",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom08",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom09",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom10",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom11",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom12",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom13",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom14",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom15",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom16",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom17",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Zoom18",
                table: "CourseMap");
        }
    }
}
