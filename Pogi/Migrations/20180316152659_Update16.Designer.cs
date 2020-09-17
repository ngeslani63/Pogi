﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Pogi.Data;
using Pogi.Entities;
using Pogi.Models;
using System;

namespace Pogi.Migrations
{
    [DbContext(typeof(PogiDbContext))]
    [Migration("20180316152659_Update16")]
    partial class Update16
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Pogi.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("Par01");

                    b.Property<int>("Par02");

                    b.Property<int>("Par03");

                    b.Property<int>("Par04");

                    b.Property<int>("Par05");

                    b.Property<int>("Par06");

                    b.Property<int>("Par07");

                    b.Property<int>("Par08");

                    b.Property<int>("Par09");

                    b.Property<int>("Par10");

                    b.Property<int>("Par11");

                    b.Property<int>("Par12");

                    b.Property<int>("Par13");

                    b.Property<int>("Par14");

                    b.Property<int>("Par15");

                    b.Property<int>("Par16");

                    b.Property<int>("Par17");

                    b.Property<int>("Par18");

                    b.Property<int>("ParIn");

                    b.Property<int>("ParOut");

                    b.Property<int>("ParTotal");

                    b.Property<string>("Phone");

                    b.Property<int>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("Pogi.Entities.CourseDetail", b =>
                {
                    b.Property<int>("CourseId");

                    b.Property<string>("Color");

                    b.Property<int>("Rating");

                    b.Property<int>("Slope");

                    b.Property<int>("Yards01");

                    b.Property<int>("Yards02");

                    b.Property<int>("Yards03");

                    b.Property<int>("Yards04");

                    b.Property<int>("Yards05");

                    b.Property<int>("Yards06");

                    b.Property<int>("Yards07");

                    b.Property<int>("Yards08");

                    b.Property<int>("Yards09");

                    b.Property<int>("Yards10");

                    b.Property<int>("Yards11");

                    b.Property<int>("Yards12");

                    b.Property<int>("Yards13");

                    b.Property<int>("Yards14");

                    b.Property<int>("Yards15");

                    b.Property<int>("Yards16");

                    b.Property<int>("Yards17");

                    b.Property<int>("Yards18");

                    b.Property<int>("YardsIn");

                    b.Property<int>("YardsOut");

                    b.Property<int>("YardsTotal");

                    b.HasKey("CourseId", "Color");

                    b.HasAlternateKey("Color", "CourseId");

                    b.ToTable("CourseDetail");
                });

            modelBuilder.Entity("Pogi.Entities.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<float>("CurrHandicap");

                    b.Property<string>("EmailAddr1st")
                        .IsRequired();

                    b.Property<string>("EmailAddr2nd");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("GhinNumber");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("MaritalStatus");

                    b.Property<int>("MemberType");

                    b.Property<string>("Phone1st");

                    b.Property<int>("Phone1stType");

                    b.Property<string>("Phone2nd");

                    b.Property<int>("Phone2ndType");

                    b.Property<string>("Profession");

                    b.Property<int>("RecordStatus");

                    b.Property<bool>("RoleAdminCourse");

                    b.Property<bool>("RoleAdminRoot");

                    b.Property<bool>("RoleAdminTeeTime");

                    b.Property<bool>("RoleAdminUser");

                    b.Property<int>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("MemberId");

                    b.HasIndex("EmailAddr1st")
                        .IsUnique();

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Pogi.Entities.Player", b =>
                {
                    b.Property<int>("PlayId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuestName");

                    b.Property<int>("MemberId");

                    b.Property<int>("Order");

                    b.Property<DateTime>("PlayDate");

                    b.Property<int>("preferTeeTimeId1");

                    b.Property<int>("preferTeeTimeId2");

                    b.Property<int>("preferTeeTimeId3");

                    b.HasKey("PlayId");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Pogi.Entities.Score", b =>
                {
                    b.Property<int>("ScoreId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AttestedTS");

                    b.Property<string>("Color");

                    b.Property<int>("CourseId");

                    b.Property<int>("EnteredById");

                    b.Property<int>("Hole01");

                    b.Property<int>("Hole02");

                    b.Property<int>("Hole03");

                    b.Property<int>("Hole04");

                    b.Property<int>("Hole05");

                    b.Property<int>("Hole06");

                    b.Property<int>("Hole07");

                    b.Property<int>("Hole08");

                    b.Property<int>("Hole09");

                    b.Property<int>("Hole10");

                    b.Property<int>("Hole11");

                    b.Property<int>("Hole12");

                    b.Property<int>("Hole13");

                    b.Property<int>("Hole14");

                    b.Property<int>("Hole15");

                    b.Property<int>("Hole16");

                    b.Property<int>("Hole17");

                    b.Property<int>("Hole18");

                    b.Property<int>("HoleIn");

                    b.Property<int>("HoleOut");

                    b.Property<int>("HoleTotal");

                    b.Property<int>("MemberId");

                    b.Property<DateTime>("ScoreTS");

                    b.Property<int>("TeeTimeId");

                    b.HasKey("ScoreId");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("Pogi.Entities.TeeAssign", b =>
                {
                    b.Property<int>("TeeAssignId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuestName");

                    b.Property<int>("MemberId");

                    b.Property<bool>("NoShow");

                    b.Property<int>("Order");

                    b.Property<int>("RecordStatus");

                    b.Property<int>("TeeTimeId");

                    b.HasKey("TeeAssignId");

                    b.HasIndex("TeeTimeId", "MemberId");

                    b.ToTable("TeeAssign");
                });

            modelBuilder.Entity("Pogi.Entities.TeeTime", b =>
                {
                    b.Property<int>("TeeTimeId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoAssign");

                    b.Property<int>("CourseId");

                    b.Property<int>("NumPlayers");

                    b.Property<int>("ReservedById");

                    b.Property<DateTime>("TeeTimeTS");

                    b.HasKey("TeeTimeId");

                    b.ToTable("TeeTime");
                });
#pragma warning restore 612, 618
        }
    }
}
