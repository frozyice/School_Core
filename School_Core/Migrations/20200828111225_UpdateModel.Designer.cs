﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using School_Core.Contexts;

namespace School_Core.Migrations
{
    [DbContext(typeof(SchoolCoreDbContext))]
    [Migration("20200828111225_UpdateModel")]
    partial class UpdateModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School_Core.Domain.Models.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<Guid>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LectureId");

                    b.HasIndex("StudentId");

                    b.ToTable("Enrollments");
                });

            modelBuilder.Entity("School_Core.Domain.Models.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CanTakeFromYear")
                        .HasColumnType("int");

                    b.Property<int>("FieldOfStudy")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeacherId");

                    b.ToTable("Lectures");

                    b.HasData(
                        new
                        {
                            Id = new Guid("0ff2c924-41e6-4d92-9d95-457f4342781b"),
                            CanTakeFromYear = 2,
                            FieldOfStudy = 0,
                            Name = "Philosophy",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("fa66fdfb-c0e8-437b-8de6-224d8aee7d78"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 0,
                            Name = "Sociology",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("83d889f8-8a94-4fb0-8363-0bf6f79e7356"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 1,
                            Name = "Introduction To Common Law",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("33f9058c-16be-4049-84c0-1d3f5b91b3ea"),
                            CanTakeFromYear = 2,
                            FieldOfStudy = 1,
                            Name = "Constitutional Law",
                            Status = 0
                        });
                });

            modelBuilder.Entity("School_Core.Domain.Models.Student", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Field")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfStudy")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = new Guid("66624204-fe88-4b57-accd-e1fd6f0277b4"),
                            Field = 0,
                            Name = "Angus",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("f043978a-cb8c-49b0-bf85-c7534786e245"),
                            Field = 1,
                            Name = "Kane",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("08d3e560-b293-4213-8e75-ddcbc2a3bd1e"),
                            Field = 1,
                            Name = "Lian",
                            YearOfStudy = 2
                        },
                        new
                        {
                            Id = new Guid("c099abd3-be5c-41f8-a598-626211962830"),
                            Field = 0,
                            Name = "Alissa",
                            YearOfStudy = 2
                        });
                });

            modelBuilder.Entity("School_Core.Domain.Models.Teacher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Teachers");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2cb96790-2002-4c4e-a1fa-1747f934a40a"),
                            Name = "Freestone"
                        },
                        new
                        {
                            Id = new Guid("ff7da57e-8b41-435f-bf2b-c74bc21ac441"),
                            Name = "Richmont"
                        },
                        new
                        {
                            Id = new Guid("9fc79bd3-de61-4ac8-ab3b-342bca63b6a4"),
                            Name = "Laker"
                        },
                        new
                        {
                            Id = new Guid("28b15ed2-4ae0-4121-9831-c74a4dfbd95f"),
                            Name = "McCarroll"
                        });
                });

            modelBuilder.Entity("School_Core.Domain.Models.Enrollment", b =>
                {
                    b.HasOne("School_Core.Domain.Models.Lecture", null)
                        .WithMany("Enrollments")
                        .HasForeignKey("LectureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("School_Core.Domain.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("School_Core.Domain.Models.Lecture", b =>
                {
                    b.HasOne("School_Core.Domain.Models.Teacher", "Teacher")
                        .WithMany()
                        .HasForeignKey("TeacherId");
                });
#pragma warning restore 612, 618
        }
    }
}
