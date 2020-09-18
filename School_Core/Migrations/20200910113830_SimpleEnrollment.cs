using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class SimpleEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments");

            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments");

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

            migrationBuilder.AlterColumn<Guid>(name: "StudentId", table: "Enrollments", nullable: true, oldClrType: typeof(Guid), oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(name: "LectureId", table: "Enrollments", nullable: true, oldClrType: typeof(Guid), oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(name: "Grade", table: "Enrollments", nullable: true, oldClrType: typeof(int), oldType: "int");

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("0a537687-b5af-421e-8ec9-c92d88594e7e"), 2, 0, "Philosophy", 0, null},
                    {new Guid("b5b76767-d0a1-4a87-994f-cc93666b3b35"), 1, 0, "Sociology", 0, null},
                    {new Guid("2b92d281-3584-4495-ab95-57623f6f096a"), 1, 1, "Introduction To Common Law", 0, null},
                    {new Guid("726d1094-b6fb-4158-bcde-47a8458f1a35"), 2, 1, "Constitutional Law", 0, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("f01cca26-c424-4c9c-a610-254569cef6cf"), 0, "Angus", 1}, {new Guid("13ede6f1-a831-4b07-905e-59444ffc81bd"), 1, "Kane", 1},
                    {new Guid("c3157bdb-dee9-4906-9d96-d25c95b3c5a6"), 1, "Lian", 2}, {new Guid("20f90926-6707-48d2-b010-98a499e81027"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("138332c5-8e5b-47b1-bc49-ea53ad58d43f"), "Freestone"}, {new Guid("0995b919-43f7-4b2e-bfe7-a254ca6497e7"), "Richmont"},
                    {new Guid("86274afc-9ccf-4873-8aaf-994491aca101"), "Laker"}, {new Guid("623d2cc2-18f4-4397-950d-846a6aac7bdf"), "McCarroll"}
                });

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments", column: "LectureId", principalTable: "Lectures", principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments", column: "StudentId", principalTable: "Students", principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments");

            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments");

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("0a537687-b5af-421e-8ec9-c92d88594e7e"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("2b92d281-3584-4495-ab95-57623f6f096a"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("726d1094-b6fb-4158-bcde-47a8458f1a35"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("b5b76767-d0a1-4a87-994f-cc93666b3b35"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("13ede6f1-a831-4b07-905e-59444ffc81bd"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("20f90926-6707-48d2-b010-98a499e81027"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("c3157bdb-dee9-4906-9d96-d25c95b3c5a6"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("f01cca26-c424-4c9c-a610-254569cef6cf"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("0995b919-43f7-4b2e-bfe7-a254ca6497e7"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("138332c5-8e5b-47b1-bc49-ea53ad58d43f"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("623d2cc2-18f4-4397-950d-846a6aac7bdf"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("86274afc-9ccf-4873-8aaf-994491aca101"));

            migrationBuilder.AlterColumn<Guid>(name: "StudentId", table: "Enrollments", type: "uniqueidentifier", nullable: false, oldClrType: typeof(Guid), oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(name: "LectureId", table: "Enrollments", type: "uniqueidentifier", nullable: false, oldClrType: typeof(Guid), oldNullable: true);

            migrationBuilder.AlterColumn<int>(name: "Grade", table: "Enrollments", type: "int", nullable: false, oldClrType: typeof(string), oldNullable: true);

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

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments", column: "LectureId", principalTable: "Lectures", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments", column: "StudentId", principalTable: "Students", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}