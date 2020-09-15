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
    [Migration("20200914153600_UpdateEnrolmentModel")]
    partial class UpdateEnrolmentModel
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
                            Id = new Guid("b3a151db-da23-4b3e-bd6d-6ae2f5c17b4e"),
                            CanTakeFromYear = 2,
                            FieldOfStudy = 0,
                            Name = "Philosophy",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("2eb93c30-c0fa-4e76-96fb-6eff820c4083"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 0,
                            Name = "Sociology",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("af040f08-864b-4e61-8c7a-5408a950d1bf"),
                            CanTakeFromYear = 1,
                            FieldOfStudy = 1,
                            Name = "Introduction To Common Law",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("743a5577-d8f5-4074-a261-fc41d73e68f9"),
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
                            Id = new Guid("e10b638a-97b2-4787-bb1d-24c47efeb9b9"),
                            Field = 0,
                            Name = "Angus",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("081f7caf-10b1-484e-bc28-7869ca000bec"),
                            Field = 1,
                            Name = "Kane",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("f6623f44-c365-409d-bc51-f4b518c34abb"),
                            Field = 1,
                            Name = "Lian",
                            YearOfStudy = 2
                        },
                        new
                        {
                            Id = new Guid("7637fd90-2dd2-46ff-9eb0-6c7cb77946eb"),
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
                            Id = new Guid("4e0c6473-b891-49c7-974b-36b5f3fabfd8"),
                            Name = "Freestone"
                        },
                        new
                        {
                            Id = new Guid("afa997d6-c151-443c-b81d-c4cddfb4126e"),
                            Name = "Richmont"
                        },
                        new
                        {
                            Id = new Guid("f0fb11b0-455f-4cf5-837b-c8a7ae5dcf47"),
                            Name = "Laker"
                        },
                        new
                        {
                            Id = new Guid("a27ac51a-769f-4b25-a4de-9c1189ef0a59"),
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
