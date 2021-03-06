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
    [Migration("20200827124724_UpdateSeed")]
    partial class UpdateSeed
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

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("School_Core.Domain.Models.Lecture", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CanTakeFromYear")
                        .HasColumnType("int");

                    b.Property<int>("Field")
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
                            Id = new Guid("5ccf2c61-fbd2-4c2a-a9d0-53c162738934"),
                            CanTakeFromYear = 2,
                            Field = 0,
                            Name = "Philosophy",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("155f25dc-76c2-46c4-a44f-59d8bace6b90"),
                            CanTakeFromYear = 1,
                            Field = 0,
                            Name = "Sociology",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("a4048b5c-9a23-4fe3-b7ad-7c59e9a2056d"),
                            CanTakeFromYear = 1,
                            Field = 1,
                            Name = "Introduction To Common Law",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("79219936-9658-4d3f-87a4-fbe30a52b627"),
                            CanTakeFromYear = 2,
                            Field = 1,
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
                            Id = new Guid("b2f313b9-25a2-4867-b066-f5046545ba19"),
                            Field = 0,
                            Name = "Angus",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("f612cf08-3726-4ba3-938a-e08f66080f63"),
                            Field = 1,
                            Name = "Kane",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("4cc8323a-aca2-46ff-be78-495001f50742"),
                            Field = 1,
                            Name = "Lian",
                            YearOfStudy = 2
                        },
                        new
                        {
                            Id = new Guid("5e871c48-2828-4e96-b141-57f64b51f7ec"),
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
                            Id = new Guid("f792b751-eb28-4939-a2c2-9b82b3309e2f"),
                            Name = "Freestone"
                        },
                        new
                        {
                            Id = new Guid("183a7a3b-e852-4b40-8d10-bbcf0b8a1c97"),
                            Name = "Richmont"
                        },
                        new
                        {
                            Id = new Guid("7d914709-f538-4d3e-a520-c2fcaadfcc70"),
                            Name = "Laker"
                        },
                        new
                        {
                            Id = new Guid("22b87db9-7df9-4e5b-88e6-212c9ffd4233"),
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
