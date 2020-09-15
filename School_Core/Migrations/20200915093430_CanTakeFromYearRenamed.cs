using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class CanTakeFromYearRenamed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("5209838c-7489-4dd9-8fcb-8b4cb7cfce14"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("8ccf27fe-f29a-42bf-9a5f-e4840b228374"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("98cc43ed-0440-440a-8b47-38f9308bdd32"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("cf97f66d-a4a6-4a17-8c04-ee37d126f117"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("163549b2-846e-465e-9beb-c9a8cc3d00df"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3b0c9eda-8d49-4abf-99f0-a19e25fdc586"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("784dd62a-5994-4129-9918-98d766e3cb39"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("db7c19af-69da-4387-a592-e7e0b928feff"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("1eddbe3b-b4b8-4b70-a7c3-c35f9440a48d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("592ad44a-c727-497b-8c7d-596b5883d35c"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("5fd3e635-801e-4ebe-ad05-35d05d44669e"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("77f79037-803a-41ac-a87d-2ec60a6db847"));

            migrationBuilder.DropColumn(
                name: "CanTakeFromYear",
                table: "Lectures");

            migrationBuilder.AddColumn<int>(
                name: "EnrollableFromYear",
                table: "Lectures",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "EnrollableFromYear",
                table: "Lectures");

            migrationBuilder.AddColumn<int>(
                name: "CanTakeFromYear",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("5209838c-7489-4dd9-8fcb-8b4cb7cfce14"), 2, 0, "Philosophy", 0, null },
                    { new Guid("cf97f66d-a4a6-4a17-8c04-ee37d126f117"), 1, 0, "Sociology", 0, null },
                    { new Guid("8ccf27fe-f29a-42bf-9a5f-e4840b228374"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("98cc43ed-0440-440a-8b47-38f9308bdd32"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FieldOfStudy", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("db7c19af-69da-4387-a592-e7e0b928feff"), 0, "Angus", 1 },
                    { new Guid("784dd62a-5994-4129-9918-98d766e3cb39"), 1, "Kane", 1 },
                    { new Guid("163549b2-846e-465e-9beb-c9a8cc3d00df"), 1, "Lian", 2 },
                    { new Guid("3b0c9eda-8d49-4abf-99f0-a19e25fdc586"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("77f79037-803a-41ac-a87d-2ec60a6db847"), "Freestone" },
                    { new Guid("1eddbe3b-b4b8-4b70-a7c3-c35f9440a48d"), "Richmont" },
                    { new Guid("592ad44a-c727-497b-8c7d-596b5883d35c"), "Laker" },
                    { new Guid("5fd3e635-801e-4ebe-ad05-35d05d44669e"), "McCarroll" }
                });
        }
    }
}
