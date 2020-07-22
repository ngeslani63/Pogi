using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update63 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Hdg01",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg02",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg03",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg04",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg05",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg06",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg07",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg08",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg09",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg10",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg11",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg12",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg13",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg14",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg15",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg16",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg17",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "Hdg18",
                table: "CourseMap",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hdg01",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg02",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg03",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg04",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg05",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg06",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg07",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg08",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg09",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg10",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg11",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg12",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg13",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg14",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg15",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg16",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg17",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "Hdg18",
                table: "CourseMap");
        }
    }
}
