using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class updateDateInSickLeaveModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("61e0f9a0-365e-47e4-9885-0ed2f3720da2"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("b8663622-9c67-4337-b591-b8f5f0b03927"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("daa6cd55-d7a4-4bb5-aec2-4fa850d6b801"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("fa8b9a86-88a8-451e-bba9-26057996839a"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("33138e2a-33d2-422a-84c0-7c01a00b60b2"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("cd6eee57-413f-45d2-ae1a-8fdfe57566e2"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f15fae68-49b8-4969-b125-88214a57d78e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f76d3797-2519-4f64-8063-da55e0631bec"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("42ef120b-baf9-4c2d-baa6-d58e3eecb5ba"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("497913eb-86c3-4385-8454-9f6c04668808"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a23dd001-f022-4bf5-8139-55f1209311d8"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b2d38d96-bd43-4e01-9ba1-f465f2912605"));

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "EnrollableFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("06f95cf6-29bb-47c3-a2a6-6fc79520c058"), 2, 0, "Philosophy", 0, null },
                    { new Guid("105a0074-cf7d-4696-a460-1d9a7568e6db"), 1, 0, "Sociology", 0, null },
                    { new Guid("e0289dcc-6422-4a47-867c-40947062f7e4"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("5d48f64c-280f-4d42-b7ec-86e71333a8f5"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FieldOfStudy", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("b07515f3-41cc-499d-8763-62b11143e34f"), 0, "Angus", 1 },
                    { new Guid("fb03977f-74cf-4078-b760-4aa60fb3a99d"), 1, "Kane", 1 },
                    { new Guid("7a724934-9161-4244-b7aa-345755b58c6b"), 1, "Lian", 2 },
                    { new Guid("93352d36-787f-447e-b1a4-b2ced583b47e"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("48c86447-0bef-4703-8103-433ee4aa72eb"), "Freestone" },
                    { new Guid("af520247-a2a3-4f80-bdb1-c4680d8426d4"), "Richmont" },
                    { new Guid("f2c43c99-ca18-49fd-90f8-48897d2a1c4c"), "Laker" },
                    { new Guid("232f5efd-6004-4f7f-95fb-96e9d4fcca21"), "McCarroll" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("06f95cf6-29bb-47c3-a2a6-6fc79520c058"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("105a0074-cf7d-4696-a460-1d9a7568e6db"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("5d48f64c-280f-4d42-b7ec-86e71333a8f5"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("e0289dcc-6422-4a47-867c-40947062f7e4"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("7a724934-9161-4244-b7aa-345755b58c6b"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("93352d36-787f-447e-b1a4-b2ced583b47e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("b07515f3-41cc-499d-8763-62b11143e34f"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("fb03977f-74cf-4078-b760-4aa60fb3a99d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("232f5efd-6004-4f7f-95fb-96e9d4fcca21"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("48c86447-0bef-4703-8103-433ee4aa72eb"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("af520247-a2a3-4f80-bdb1-c4680d8426d4"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("f2c43c99-ca18-49fd-90f8-48897d2a1c4c"));

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "EnrollableFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("daa6cd55-d7a4-4bb5-aec2-4fa850d6b801"), 2, 0, "Philosophy", 0, null },
                    { new Guid("61e0f9a0-365e-47e4-9885-0ed2f3720da2"), 1, 0, "Sociology", 0, null },
                    { new Guid("fa8b9a86-88a8-451e-bba9-26057996839a"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("b8663622-9c67-4337-b591-b8f5f0b03927"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FieldOfStudy", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("f15fae68-49b8-4969-b125-88214a57d78e"), 0, "Angus", 1 },
                    { new Guid("f76d3797-2519-4f64-8063-da55e0631bec"), 1, "Kane", 1 },
                    { new Guid("33138e2a-33d2-422a-84c0-7c01a00b60b2"), 1, "Lian", 2 },
                    { new Guid("cd6eee57-413f-45d2-ae1a-8fdfe57566e2"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("a23dd001-f022-4bf5-8139-55f1209311d8"), "Freestone" },
                    { new Guid("497913eb-86c3-4385-8454-9f6c04668808"), "Richmont" },
                    { new Guid("b2d38d96-bd43-4e01-9ba1-f465f2912605"), "Laker" },
                    { new Guid("42ef120b-baf9-4c2d-baa6-d58e3eecb5ba"), "McCarroll" }
                });
        }
    }
}
