using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("a1b54f8b-7cb5-492a-b7b7-e274c27a56b6"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("a625a775-58ce-424f-8b83-c1685d6f3366"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("ae93d6f0-fcf4-4256-8c04-1939aa3d8e31"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("d3f3bd63-b5e4-49b4-9b82-087a4caa8a2a"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("0276092c-67f0-43d2-bcd7-8a7af869a4f5"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("86ce4f83-190e-4b70-a540-67bae7687d3c"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("df6d8f00-0d7b-460b-b422-e9af773fe6a5"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f8a7fa7a-9b64-47c1-bfbc-45248c10de17"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("155f0dcd-a845-419f-9028-00a02c99f088"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("811104f6-d1cf-483f-9dc7-5834d99c03ea"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("8fa34838-23c8-42e1-850f-7394d9ffccf5"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("c01e9ea1-23f5-4215-930d-4c3d60262187"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                    { new Guid("a625a775-58ce-424f-8b83-c1685d6f3366"), 2, 0, "Philosophy", 0, null },
                    { new Guid("ae93d6f0-fcf4-4256-8c04-1939aa3d8e31"), 1, 0, "Sociology", 0, null },
                    { new Guid("a1b54f8b-7cb5-492a-b7b7-e274c27a56b6"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("d3f3bd63-b5e4-49b4-9b82-087a4caa8a2a"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FieldOfStudy", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("df6d8f00-0d7b-460b-b422-e9af773fe6a5"), 0, "Angus", 1 },
                    { new Guid("86ce4f83-190e-4b70-a540-67bae7687d3c"), 1, "Kane", 1 },
                    { new Guid("f8a7fa7a-9b64-47c1-bfbc-45248c10de17"), 1, "Lian", 2 },
                    { new Guid("0276092c-67f0-43d2-bcd7-8a7af869a4f5"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c01e9ea1-23f5-4215-930d-4c3d60262187"), "Freestone" },
                    { new Guid("8fa34838-23c8-42e1-850f-7394d9ffccf5"), "Richmont" },
                    { new Guid("811104f6-d1cf-483f-9dc7-5834d99c03ea"), "Laker" },
                    { new Guid("155f0dcd-a845-419f-9028-00a02c99f088"), "McCarroll" }
                });
        }
    }
}
