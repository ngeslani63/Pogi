using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Course");

            migrationBuilder.RenameColumn(
                name: "Phone2",
                table: "Course",
                newName: "Phone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "Course",
                newName: "Phone2");

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Course",
                nullable: true);
        }
    }
}
