﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using School_Core.API.Contexts;

namespace School_Core.API.Migrations
{
    [DbContext(typeof(SchoolMedicalDbContext))]
    partial class SchoolMedicalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("School_Core.API.Models.Medical", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DateTo")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("StudentId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Medicals");

                    b.HasData(
                        new
                        {
                            Id = new Guid("9d97c884-5ae3-4c01-b93c-2e2c9eb3543a"),
                            DateFrom = new DateTime(2020, 11, 3, 13, 16, 54, 442, DateTimeKind.Local).AddTicks(5770),
                            Reason = "DummyReason1",
                            StudentId = new Guid("1e60a216-d15d-4ff0-bf3f-efc4b8a04a1d")
                        },
                        new
                        {
                            Id = new Guid("1dfa57eb-bad1-41e3-8dc4-d518da954daf"),
                            DateFrom = new DateTime(2020, 11, 3, 13, 16, 54, 445, DateTimeKind.Local).AddTicks(5105),
                            Reason = "DummyReason2",
                            StudentId = new Guid("666e70e0-7537-42d7-b587-0ad3e2ccce24")
                        },
                        new
                        {
                            Id = new Guid("664c3e04-9bda-43b8-80e9-492a4eda7ed3"),
                            DateFrom = new DateTime(2020, 11, 3, 13, 16, 54, 445, DateTimeKind.Local).AddTicks(5181),
                            Reason = "DummyReason3",
                            StudentId = new Guid("666e70e0-7537-42d7-b587-0ad3e2ccce24")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
