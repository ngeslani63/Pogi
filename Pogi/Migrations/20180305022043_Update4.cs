using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "recordStatus",
                table: "Member",
                newName: "RecordStatus");

            migrationBuilder.RenameColumn(
                name: "profession",
                table: "Member",
                newName: "Profession");

            migrationBuilder.RenameColumn(
                name: "memberStatus",
                table: "Member",
                newName: "MemberStatus");

            migrationBuilder.RenameColumn(
                name: "maritalStatus",
                table: "Member",
                newName: "MaritalStatus");

            migrationBuilder.RenameColumn(
                name: "ghinNumber",
                table: "Member",
                newName: "GhinNumber");

            migrationBuilder.RenameColumn(
                name: "currHandicap",
                table: "Member",
                newName: "CurrHandicap");

            migrationBuilder.AlterColumn<int>(
                name: "Phone2ndType",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Phone1stType",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RecordStatus",
                table: "Member",
                newName: "recordStatus");

            migrationBuilder.RenameColumn(
                name: "Profession",
                table: "Member",
                newName: "profession");

            migrationBuilder.RenameColumn(
                name: "MemberStatus",
                table: "Member",
                newName: "memberStatus");

            migrationBuilder.RenameColumn(
                name: "MaritalStatus",
                table: "Member",
                newName: "maritalStatus");

            migrationBuilder.RenameColumn(
                name: "GhinNumber",
                table: "Member",
                newName: "ghinNumber");

            migrationBuilder.RenameColumn(
                name: "CurrHandicap",
                table: "Member",
                newName: "currHandicap");

            migrationBuilder.AlterColumn<string>(
                name: "Phone2ndType",
                table: "Member",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "Phone1stType",
                table: "Member",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
