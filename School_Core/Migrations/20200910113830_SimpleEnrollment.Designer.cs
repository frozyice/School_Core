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
    [Migration("20200910113830_SimpleEnrollment")]
    partial class SimpleEnrollment
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School_Core.Domain.Models.Enrollment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("LectureId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("StudentId")
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
                            Id = new Guid("0a537687-b5af-421e-8ec9-c92d88594e7e"),
                            CanTakeFromYear = 2,
                            FieldOfStudy = 0,
                            Name = "Philosophy",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("b5b76767-d0a1-4a87-994f-cc93666b3b35"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 0,
                            Name = "Sociology",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("2b92d281-3584-4495-ab95-57623f6f096a"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 1,
                            Name = "Introduction To Common Law",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("726d1094-b6fb-4158-bcde-47a8458f1a35"),
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
                            Id = new Guid("f01cca26-c424-4c9c-a610-254569cef6cf"),
                            Field = 0,
                            Name = "Angus",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("13ede6f1-a831-4b07-905e-59444ffc81bd"),
                            Field = 1,
                            Name = "Kane",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("c3157bdb-dee9-4906-9d96-d25c95b3c5a6"),
                            Field = 1,
                            Name = "Lian",
                            YearOfStudy = 2
                        },
                        new
                        {
                            Id = new Guid("20f90926-6707-48d2-b010-98a499e81027"),
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
                            Id = new Guid("138332c5-8e5b-47b1-bc49-ea53ad58d43f"),
                            Name = "Freestone"
                        },
                        new
                        {
                            Id = new Guid("0995b919-43f7-4b2e-bfe7-a254ca6497e7"),
                            Name = "Richmont"
                        },
                        new
                        {
                            Id = new Guid("86274afc-9ccf-4873-8aaf-994491aca101"),
                            Name = "Laker"
                        },
                        new
                        {
                            Id = new Guid("623d2cc2-18f4-4397-950d-846a6aac7bdf"),
                            Name = "McCarroll"
                        });
                });

            modelBuilder.Entity("School_Core.Domain.Models.Enrollment", b =>
                {
                    b.HasOne("School_Core.Domain.Models.Lecture", null)
                        .WithMany("Enrollments")
                        .HasForeignKey("LectureId");

                    b.HasOne("School_Core.Domain.Models.Student", null)
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentId");
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
