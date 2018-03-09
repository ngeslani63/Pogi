using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Pogi.Migrations
{
    public partial class Update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    EmailAddr1st = table.Column<string>(nullable: false),
                    EmailAddr2nd = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 40, nullable: false),
                    LastName = table.Column<string>(maxLength: 40, nullable: false),
                    Phone1st = table.Column<string>(nullable: true),
                    Phone1stType = table.Column<string>(nullable: true),
                    Phone2nd = table.Column<string>(nullable: true),
                    Phone2ndType = table.Column<string>(nullable: true),
                    State = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true),
                    currHandicap = table.Column<int>(nullable: false),
                    ghinNumber = table.Column<int>(nullable: false),
                    maritalStatus = table.Column<string>(nullable: true),
                    memberStatus = table.Column<string>(nullable: true),
                    profession = table.Column<string>(nullable: true),
                    recordStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberId);
                });

            migrationBuilder.CreateTable(
                name: "TeeAssign",
                columns: table => new
                {
                    TeeAssignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GuestName = table.Column<string>(nullable: true),
                    MemberId = table.Column<int>(nullable: false),
                    NoShow = table.Column<bool>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    TeeTimeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeeAssign", x => x.TeeAssignId);
                });

            migrationBuilder.CreateTable(
                name: "TeeTime",
                columns: table => new
                {
                    TeeTimeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AutoAssign = table.Column<bool>(nullable: false),
                    CourseId = table.Column<int>(nullable: false),
                    NumPlayers = table.Column<int>(nullable: false),
                    ReservedById = table.Column<int>(nullable: false),
                    TeeTimeTS = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeeTime", x => x.TeeTimeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeeAssign_TeeTimeId_MemberId",
                table: "TeeAssign",
                columns: new[] { "TeeTimeId", "MemberId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "TeeAssign");

            migrationBuilder.DropTable(
                name: "TeeTime");
        }
    }
}
