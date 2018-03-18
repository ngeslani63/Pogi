using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "CourseDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTs",
                table: "CourseDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "CourseDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedTs",
                table: "CourseDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTs",
                table: "Course",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "LastUpdatedBy",
                table: "Course",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedTs",
                table: "Course",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CourseDetail");

            migrationBuilder.DropColumn(
                name: "CreatedTs",
                table: "CourseDetail");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "CourseDetail");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTs",
                table: "CourseDetail");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CreatedTs",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LastUpdatedBy",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "LastUpdatedTs",
                table: "Course");
        }
    }
}
