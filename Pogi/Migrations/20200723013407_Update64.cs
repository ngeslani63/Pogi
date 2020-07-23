using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update64 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "InitLat01",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat02",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat03",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat04",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat05",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat06",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat07",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat08",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat09",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat10",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat11",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat12",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat13",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat14",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat15",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat16",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat17",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLat18",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng01",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng02",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng03",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng04",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng05",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng06",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng07",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng08",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng09",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng10",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng11",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng12",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng13",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng14",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng15",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng16",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng17",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "InitLng18",
                table: "CourseMap",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InitLat01",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat02",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat03",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat04",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat05",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat06",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat07",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat08",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat09",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat10",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat11",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat12",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat13",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat14",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat15",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat16",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat17",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLat18",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng01",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng02",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng03",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng04",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng05",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng06",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng07",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng08",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng09",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng10",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng11",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng12",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng13",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng14",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng15",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng16",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng17",
                table: "CourseMap");

            migrationBuilder.DropColumn(
                name: "InitLng18",
                table: "CourseMap");
        }
    }
}
