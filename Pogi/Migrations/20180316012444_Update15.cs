using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAddr1st",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.CreateIndex(
                name: "IX_Member_EmailAddr1st",
                table: "Member",
                column: "EmailAddr1st",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Member_EmailAddr1st",
                table: "Member");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddr1st",
                table: "Member",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
