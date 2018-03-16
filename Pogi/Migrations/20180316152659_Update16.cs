using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RoleAdminCourse",
                table: "Member",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RoleAdminRoot",
                table: "Member",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RoleAdminTeeTime",
                table: "Member",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RoleAdminUser",
                table: "Member",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleAdminCourse",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "RoleAdminRoot",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "RoleAdminTeeTime",
                table: "Member");

            migrationBuilder.DropColumn(
                name: "RoleAdminUser",
                table: "Member");
        }
    }
}
