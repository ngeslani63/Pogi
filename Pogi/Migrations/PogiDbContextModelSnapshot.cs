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
    partial class PogiDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedTs");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedTs");

                    b.Property<int>("NumTees");

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

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedTs");

                    b.Property<string>("LastUpdatedBy");

                    b.Property<DateTime>("LastUpdatedTs");

                    b.Property<float>("Rating");

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

            modelBuilder.Entity("Pogi.Entities.CourseHandicap", b =>
                {
                    b.Property<int>("CourseHandicapId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("LadiesHcp01");

                    b.Property<int>("LadiesHcp02");

                    b.Property<int>("LadiesHcp03");

                    b.Property<int>("LadiesHcp04");

                    b.Property<int>("LadiesHcp05");

                    b.Property<int>("LadiesHcp06");

                    b.Property<int>("LadiesHcp07");

                    b.Property<int>("LadiesHcp08");

                    b.Property<int>("LadiesHcp09");

                    b.Property<int>("LadiesHcp10");

                    b.Property<int>("LadiesHcp11");

                    b.Property<int>("LadiesHcp12");

                    b.Property<int>("LadiesHcp13");

                    b.Property<int>("LadiesHcp14");

                    b.Property<int>("LadiesHcp15");

                    b.Property<int>("LadiesHcp16");

                    b.Property<int>("LadiesHcp17");

                    b.Property<int>("LadiesHcp18");

                    b.Property<int>("MenHcp01");

                    b.Property<int>("MenHcp02");

                    b.Property<int>("MenHcp03");

                    b.Property<int>("MenHcp04");

                    b.Property<int>("MenHcp05");

                    b.Property<int>("MenHcp06");

                    b.Property<int>("MenHcp07");

                    b.Property<int>("MenHcp08");

                    b.Property<int>("MenHcp09");

                    b.Property<int>("MenHcp10");

                    b.Property<int>("MenHcp11");

                    b.Property<int>("MenHcp12");

                    b.Property<int>("MenHcp13");

                    b.Property<int>("MenHcp14");

                    b.Property<int>("MenHcp15");

                    b.Property<int>("MenHcp16");

                    b.Property<int>("MenHcp17");

                    b.Property<int>("MenHcp18");

                    b.HasKey("CourseHandicapId");

                    b.HasIndex("CourseId")
                        .IsUnique();

                    b.ToTable("CourseHandicap");
                });

            modelBuilder.Entity("Pogi.Entities.Handicap", b =>
                {
                    b.Property<int>("HandicapId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("GhinNumber");

                    b.Property<float>("HcpIndex");

                    b.HasKey("HandicapId");

                    b.ToTable("Handicap");
                });

            modelBuilder.Entity("Pogi.Entities.HandicapSchedule", b =>
                {
                    b.Property<int>("HandicapScheduleId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("RecordStatus");

                    b.Property<int>("RevisionNumber");

                    b.HasKey("HandicapScheduleId");

                    b.ToTable("HandicapSchedule");
                });

            modelBuilder.Entity("Pogi.Entities.IpGuest", b =>
                {
                    b.Property<string>("IpAddr")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("LastUpdtTs");

                    b.Property<string>("UserName");

                    b.HasKey("IpAddr");

                    b.ToTable("IpGuest");
                });

            modelBuilder.Entity("Pogi.Entities.Log2", b =>
                {
                    b.Property<int>("ActivityId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Action")
                        .IsRequired();

                    b.Property<int>("MemberId");

                    b.Property<string>("UserName");

                    b.Property<DateTime>("createdTS");

                    b.Property<string>("ipAddr");

                    b.HasKey("ActivityId");

                    b.ToTable("Log2");
                });

            modelBuilder.Entity("Pogi.Entities.Member", b =>
                {
                    b.Property<int>("MemberId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AboutMe");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<float>("CurrHandicap");

                    b.Property<string>("EmailAddr1st")
                        .IsRequired();

                    b.Property<string>("EmailAddr2nd");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("Gender");

                    b.Property<int>("GhinNumber");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(40);

                    b.Property<int>("MaritalStatus");

                    b.Property<int>("MemberStatus");

                    b.Property<string>("Phone1st");

                    b.Property<int>("Phone1stType");

                    b.Property<string>("Phone2nd");

                    b.Property<int>("Phone2ndType");

                    b.Property<string>("Profession");

                    b.Property<string>("ProfileFileName");

                    b.Property<int>("RecordStatus");

                    b.Property<bool>("RoleAdminCourse");

                    b.Property<bool>("RoleAdminRoot");

                    b.Property<bool>("RoleAdminScore");

                    b.Property<bool>("RoleAdminTeeTime");

                    b.Property<bool>("RoleAdminTour");

                    b.Property<bool>("RoleAdminUser");

                    b.Property<int>("State");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("MemberId");

                    b.HasIndex("EmailAddr1st")
                        .IsUnique();

                    b.HasIndex("GhinNumber");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Pogi.Entities.Player", b =>
                {
                    b.Property<int>("PlayId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AssignedTee");

                    b.Property<DateTime>("ConfirmDate");

                    b.Property<bool>("Confirmed");

                    b.Property<int>("EnteredById");

                    b.Property<string>("GuestName");

                    b.Property<int>("MemberId");

                    b.Property<int>("Order");

                    b.Property<DateTime>("PlayDate");

                    b.Property<int>("PlayerState");

                    b.Property<int>("RegistrationMethod");

                    b.Property<string>("WithdrawReason");

                    b.Property<bool>("Withdrawn");

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

                    b.Property<string>("AboutGame")
                        .HasMaxLength(200);

                    b.Property<int>("Albatross");

                    b.Property<int>("Birdies");

                    b.Property<int>("Bogeys");

                    b.Property<string>("Color");

                    b.Property<int>("CourseId");

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreatedTs");

                    b.Property<int>("Disaster");

                    b.Property<int>("DoubleBogeys");

                    b.Property<int>("Eagles");

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

                    b.Property<int>("HoleInOnes");

                    b.Property<int>("HoleOut");

                    b.Property<int>("HoleTotal");

                    b.Property<string>("LastUpdatedBy")
                        .ValueGeneratedOnAddOrUpdate();

                    b.Property<DateTime>("LastUpdatedTs");

                    b.Property<int>("MaxScore");

                    b.Property<int>("MemberId");

                    b.Property<int>("NetScore");

                    b.Property<int>("Pars");

                    b.Property<int>("QuadBogeys");

                    b.Property<DateTime>("ScoreDate");

                    b.Property<string>("Tiebreaker");

                    b.Property<bool>("TourEvent");

                    b.Property<int>("TourId");

                    b.Property<int>("TourScore");

                    b.Property<int>("TripleBogeys");

                    b.HasKey("ScoreId");

                    b.ToTable("Score");
                });

            modelBuilder.Entity("Pogi.Entities.TeeAssign", b =>
                {
                    b.Property<int>("TeeAssignId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Group");

                    b.Property<string>("GuestName");

                    b.Property<int>("MemberId");

                    b.Property<bool>("NoShow");

                    b.Property<int>("Order");

                    b.Property<int>("PlayId");

                    b.Property<int>("RecordStatus");

                    b.Property<int>("TeeTimeId");

                    b.Property<string>("color");

                    b.HasKey("TeeAssignId");

                    b.HasIndex("TeeTimeId", "MemberId");

                    b.ToTable("TeeAssign");
                });

            modelBuilder.Entity("Pogi.Entities.TeeTime", b =>
                {
                    b.Property<int>("TeeTimeId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AutoAssign");

                    b.Property<string>("Color");

                    b.Property<int>("CourseId");

                    b.Property<int>("LockWithdrawDays");

                    b.Property<bool>("MajorTour");

                    b.Property<int>("NumPlayers");

                    b.Property<int>("ReservedById");

                    b.Property<int>("TeeTimeInterval");

                    b.Property<DateTime>("TeeTimeTS");

                    b.HasKey("TeeTimeId");

                    b.ToTable("TeeTime");
                });

            modelBuilder.Entity("Pogi.Entities.Tour", b =>
                {
                    b.Property<int>("TourId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AllowMultiTee");

                    b.Property<int>("BaseColor");

                    b.Property<float>("HcpAllowPct");

                    b.Property<int>("ScorerType");

                    b.Property<DateTime>("TourDate");

                    b.Property<string>("TourName")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<int>("TourType");

                    b.HasKey("TourId");

                    b.ToTable("Tour");
                });

            modelBuilder.Entity("Pogi.Entities.TourDay", b =>
                {
                    b.Property<int>("TourDayId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("TourDate");

                    b.Property<int>("TourId");

                    b.HasKey("TourDayId");

                    b.ToTable("TourDay");
                });
#pragma warning restore 612, 618
        }
    }
}
