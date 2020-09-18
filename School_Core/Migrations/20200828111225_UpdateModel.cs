using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("05de291f-7029-4601-a523-c5c46c4531d4"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("1e236aca-5cbc-462c-b008-79bc01aada75"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("8f22e343-a140-4b4c-95ec-a8da810a915c"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("c096d152-be82-46c8-b807-fd2bc2b37262"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("79023ccf-b6ef-4b64-80dd-c820f9dc4368"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("db550284-3acd-45d0-acd9-00db6b247d9a"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("e17090a3-379e-4418-9e28-aec554be73da"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("eca4bf02-1e0e-41cb-ac27-3fed787b0df7"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("12c55362-2cfa-4c5f-b64e-2530657db4be"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("5ce04092-3565-4b32-b65a-c6f6f8f9d631"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("5cebf6ff-1422-4777-a242-c5b7a1f4d18c"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("91ecc3e3-7478-4279-924a-ea62fc06bd67"));

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("0ff2c924-41e6-4d92-9d95-457f4342781b"), 2, 0, "Philosophy", 0, null},
                    {new Guid("fa66fdfb-c0e8-437b-8de6-224d8aee7d78"), 1, 0, "Sociology", 0, null},
                    {new Guid("83d889f8-8a94-4fb0-8363-0bf6f79e7356"), 1, 1, "Introduction To Common Law", 0, null},
                    {new Guid("33f9058c-16be-4049-84c0-1d3f5b91b3ea"), 2, 1, "Constitutional Law", 0, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("66624204-fe88-4b57-accd-e1fd6f0277b4"), 0, "Angus", 1}, {new Guid("f043978a-cb8c-49b0-bf85-c7534786e245"), 1, "Kane", 1},
                    {new Guid("08d3e560-b293-4213-8e75-ddcbc2a3bd1e"), 1, "Lian", 2}, {new Guid("c099abd3-be5c-41f8-a598-626211962830"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("2cb96790-2002-4c4e-a1fa-1747f934a40a"), "Freestone"}, {new Guid("ff7da57e-8b41-435f-bf2b-c74bc21ac441"), "Richmont"},
                    {new Guid("9fc79bd3-de61-4ac8-ab3b-342bca63b6a4"), "Laker"}, {new Guid("28b15ed2-4ae0-4121-9831-c74a4dfbd95f"), "McCarroll"}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("0ff2c924-41e6-4d92-9d95-457f4342781b"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("33f9058c-16be-4049-84c0-1d3f5b91b3ea"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("83d889f8-8a94-4fb0-8363-0bf6f79e7356"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("fa66fdfb-c0e8-437b-8de6-224d8aee7d78"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("08d3e560-b293-4213-8e75-ddcbc2a3bd1e"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("66624204-fe88-4b57-accd-e1fd6f0277b4"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("c099abd3-be5c-41f8-a598-626211962830"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("f043978a-cb8c-49b0-bf85-c7534786e245"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("28b15ed2-4ae0-4121-9831-c74a4dfbd95f"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("2cb96790-2002-4c4e-a1fa-1747f934a40a"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("9fc79bd3-de61-4ac8-ab3b-342bca63b6a4"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("ff7da57e-8b41-435f-bf2b-c74bc21ac441"));

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("c096d152-be82-46c8-b807-fd2bc2b37262"), 2, 0, "Philosophy", 1, null},
                    {new Guid("05de291f-7029-4601-a523-c5c46c4531d4"), 1, 0, "Sociology", 1, null},
                    {new Guid("8f22e343-a140-4b4c-95ec-a8da810a915c"), 1, 1, "Introduction To Common Law", 1, null},
                    {new Guid("1e236aca-5cbc-462c-b008-79bc01aada75"), 2, 1, "Constitutional Law", 1, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("db550284-3acd-45d0-acd9-00db6b247d9a"), 0, "Angus", 1}, {new Guid("eca4bf02-1e0e-41cb-ac27-3fed787b0df7"), 1, "Kane", 1},
                    {new Guid("e17090a3-379e-4418-9e28-aec554be73da"), 1, "Lian", 2}, {new Guid("79023ccf-b6ef-4b64-80dd-c820f9dc4368"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("5cebf6ff-1422-4777-a242-c5b7a1f4d18c"), "Freestone"}, {new Guid("12c55362-2cfa-4c5f-b64e-2530657db4be"), "Richmont"},
                    {new Guid("5ce04092-3565-4b32-b65a-c6f6f8f9d631"), "Laker"}, {new Guid("91ecc3e3-7478-4279-924a-ea62fc06bd67"), "McCarroll"}
                });
        }
    }
}