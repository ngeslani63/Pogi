using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AssignedTee",
                table: "Player",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "Player",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "EnteredById",
                table: "Player",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "WithdrawReason",
                table: "Player",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Withdrawn",
                table: "Player",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTee",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "EnteredById",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "WithdrawReason",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "Withdrawn",
                table: "Player");
        }
    }
}
