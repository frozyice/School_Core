using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateStudentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("2eb93c30-c0fa-4e76-96fb-6eff820c4083"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("743a5577-d8f5-4074-a261-fc41d73e68f9"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("af040f08-864b-4e61-8c7a-5408a950d1bf"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("b3a151db-da23-4b3e-bd6d-6ae2f5c17b4e"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("081f7caf-10b1-484e-bc28-7869ca000bec"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("7637fd90-2dd2-46ff-9eb0-6c7cb77946eb"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("e10b638a-97b2-4787-bb1d-24c47efeb9b9"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("f6623f44-c365-409d-bc51-f4b518c34abb"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("4e0c6473-b891-49c7-974b-36b5f3fabfd8"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("a27ac51a-769f-4b25-a4de-9c1189ef0a59"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("afa997d6-c151-443c-b81d-c4cddfb4126e"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("f0fb11b0-455f-4cf5-837b-c8a7ae5dcf47"));

            migrationBuilder.DropColumn(name: "Field", table: "Students");

            migrationBuilder.AddColumn<int>(name: "FieldOfStudy", table: "Students", nullable: false, defaultValue: 0);

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("5209838c-7489-4dd9-8fcb-8b4cb7cfce14"), 2, 0, "Philosophy", 0, null},
                    {new Guid("cf97f66d-a4a6-4a17-8c04-ee37d126f117"), 1, 0, "Sociology", 0, null},
                    {new Guid("8ccf27fe-f29a-42bf-9a5f-e4840b228374"), 1, 1, "Introduction To Common Law", 0, null},
                    {new Guid("98cc43ed-0440-440a-8b47-38f9308bdd32"), 2, 1, "Constitutional Law", 0, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "FieldOfStudy", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("db7c19af-69da-4387-a592-e7e0b928feff"), 0, "Angus", 1}, {new Guid("784dd62a-5994-4129-9918-98d766e3cb39"), 1, "Kane", 1},
                    {new Guid("163549b2-846e-465e-9beb-c9a8cc3d00df"), 1, "Lian", 2}, {new Guid("3b0c9eda-8d49-4abf-99f0-a19e25fdc586"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("77f79037-803a-41ac-a87d-2ec60a6db847"), "Freestone"}, {new Guid("1eddbe3b-b4b8-4b70-a7c3-c35f9440a48d"), "Richmont"},
                    {new Guid("592ad44a-c727-497b-8c7d-596b5883d35c"), "Laker"}, {new Guid("5fd3e635-801e-4ebe-ad05-35d05d44669e"), "McCarroll"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("5209838c-7489-4dd9-8fcb-8b4cb7cfce14"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("8ccf27fe-f29a-42bf-9a5f-e4840b228374"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("98cc43ed-0440-440a-8b47-38f9308bdd32"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("cf97f66d-a4a6-4a17-8c04-ee37d126f117"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("163549b2-846e-465e-9beb-c9a8cc3d00df"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("3b0c9eda-8d49-4abf-99f0-a19e25fdc586"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("784dd62a-5994-4129-9918-98d766e3cb39"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("db7c19af-69da-4387-a592-e7e0b928feff"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("1eddbe3b-b4b8-4b70-a7c3-c35f9440a48d"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("592ad44a-c727-497b-8c7d-596b5883d35c"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("5fd3e635-801e-4ebe-ad05-35d05d44669e"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("77f79037-803a-41ac-a87d-2ec60a6db847"));

            migrationBuilder.DropColumn(name: "FieldOfStudy", table: "Students");

            migrationBuilder.AddColumn<int>(name: "Field", table: "Students", type: "int", nullable: false, defaultValue: 0);

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("b3a151db-da23-4b3e-bd6d-6ae2f5c17b4e"), 2, 0, "Philosophy", 0, null},
                    {new Guid("2eb93c30-c0fa-4e76-96fb-6eff820c4083"), 1, 0, "Sociology", 0, null},
                    {new Guid("af040f08-864b-4e61-8c7a-5408a950d1bf"), 1, 1, "Introduction To Common Law", 0, null},
                    {new Guid("743a5577-d8f5-4074-a261-fc41d73e68f9"), 2, 1, "Constitutional Law", 0, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("e10b638a-97b2-4787-bb1d-24c47efeb9b9"), 0, "Angus", 1}, {new Guid("081f7caf-10b1-484e-bc28-7869ca000bec"), 1, "Kane", 1},
                    {new Guid("f6623f44-c365-409d-bc51-f4b518c34abb"), 1, "Lian", 2}, {new Guid("7637fd90-2dd2-46ff-9eb0-6c7cb77946eb"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("4e0c6473-b891-49c7-974b-36b5f3fabfd8"), "Freestone"}, {new Guid("afa997d6-c151-443c-b81d-c4cddfb4126e"), "Richmont"},
                    {new Guid("f0fb11b0-455f-4cf5-837b-c8a7ae5dcf47"), "Laker"}, {new Guid("a27ac51a-769f-4b25-a4de-9c1189ef0a59"), "McCarroll"}
                });
        }
    }
}