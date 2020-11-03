using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.API.Migrations
{
    public partial class AddSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SickLeaves",
                columns: new[] { "Id", "DateFrom", "DateTo", "Reason", "StudentId" },
                values: new object[] { new Guid("eb7bde3e-1c7e-4a71-bd02-3903be8f9ea2"), new DateTime(2020, 11, 3, 12, 53, 18, 202, DateTimeKind.Local).AddTicks(3797), null, "DummyReason1", new Guid("90a4faf7-33d9-4bf5-8584-1f92e80db0cb") });

            migrationBuilder.InsertData(
                table: "SickLeaves",
                columns: new[] { "Id", "DateFrom", "DateTo", "Reason", "StudentId" },
                values: new object[] { new Guid("4ccdc39b-c060-428e-9d4c-cb6b4834ed10"), new DateTime(2020, 11, 3, 12, 53, 18, 204, DateTimeKind.Local).AddTicks(9926), null, "DummyReason2", new Guid("b92bef62-3853-4b21-a6f4-c6f11bf91a9b") });

            migrationBuilder.InsertData(
                table: "SickLeaves",
                columns: new[] { "Id", "DateFrom", "DateTo", "Reason", "StudentId" },
                values: new object[] { new Guid("cceba541-85c9-4f3f-97c4-c9629e1e0200"), new DateTime(2020, 11, 3, 12, 53, 18, 204, DateTimeKind.Local).AddTicks(9968), null, "DummyReason3", new Guid("3f413ea5-a53f-472b-85da-a0d2c0afca6c") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SickLeaves",
                keyColumn: "Id",
                keyValue: new Guid("4ccdc39b-c060-428e-9d4c-cb6b4834ed10"));

            migrationBuilder.DeleteData(
                table: "SickLeaves",
                keyColumn: "Id",
                keyValue: new Guid("cceba541-85c9-4f3f-97c4-c9629e1e0200"));

            migrationBuilder.DeleteData(
                table: "SickLeaves",
                keyColumn: "Id",
                keyValue: new Guid("eb7bde3e-1c7e-4a71-bd02-3903be8f9ea2"));
        }
    }
}
