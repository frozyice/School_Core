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
    [Migration("20200827124336_ReworkSeedAndDomainModels")]
    partial class ReworkSeedAndDomainModels
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
                            Id = new Guid("3ef9c0a2-d9ed-4b0c-bcda-a980e1d017f5"),
                            CanTakeFromYear = 1,
                            Field = 0,
                            Name = "Philosophy",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("b64af4de-aa68-46e2-883e-a9103bd6d276"),
                            CanTakeFromYear = 1,
                            Field = 0,
                            Name = "Sociology",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("8e0fff1d-a30b-498e-9284-690c5d0f08ad"),
                            CanTakeFromYear = 1,
                            Field = 0,
                            Name = "Introduction To Common Law",
                            Status = 0
                        },
                        new
                        {
                            Id = new Guid("b55deb5e-a1e2-4d1a-92e1-2dc24459eeb4"),
                            CanTakeFromYear = 1,
                            Field = 0,
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
                            Id = new Guid("98dadc27-27a2-47f7-8ef9-79fcba06d36f"),
                            Field = 0,
                            Name = "Angus",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("a8c3abf1-5f89-4133-8bc8-8614b8c7e325"),
                            Field = 1,
                            Name = "Kane",
                            YearOfStudy = 1
                        },
                        new
                        {
                            Id = new Guid("3290b43d-a7fb-4fd0-9c03-cff3c82f4e18"),
                            Field = 1,
                            Name = "Lian",
                            YearOfStudy = 2
                        },
                        new
                        {
                            Id = new Guid("b524cbc0-ecb6-4a40-8536-f95d52607d2d"),
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
                            Id = new Guid("c7464cfb-1974-4fd9-a226-78a000f95c74"),
                            Name = "Freestone"
                        },
                        new
                        {
                            Id = new Guid("bbb05fbe-2d66-4997-9a81-337b6a8794bd"),
                            Name = "Richmont"
                        },
                        new
                        {
                            Id = new Guid("b2efa763-e1b0-475d-91dc-0dd534049c09"),
                            Name = "Laker"
                        },
                        new
                        {
                            Id = new Guid("5e99e9ab-d812-4d8e-abc9-bb2997aa98a3"),
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
