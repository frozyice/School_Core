using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateEnrolmentModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Lectures_LectureId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("0a537687-b5af-421e-8ec9-c92d88594e7e"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("2b92d281-3584-4495-ab95-57623f6f096a"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("726d1094-b6fb-4158-bcde-47a8458f1a35"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("b5b76767-d0a1-4a87-994f-cc93666b3b35"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("13ede6f1-a831-4b07-905e-59444ffc81bd"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("20f90926-6707-48d2-b010-98a499e81027"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("c3157bdb-dee9-4906-9d96-d25c95b3c5a6"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f01cca26-c424-4c9c-a610-254569cef6cf"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("0995b919-43f7-4b2e-bfe7-a254ca6497e7"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("138332c5-8e5b-47b1-bc49-ea53ad58d43f"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("623d2cc2-18f4-4397-950d-846a6aac7bdf"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("86274afc-9ccf-4873-8aaf-994491aca101"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "LectureId",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "Enrollments",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("b3a151db-da23-4b3e-bd6d-6ae2f5c17b4e"), 2, 0, "Philosophy", 0, null },
                    { new Guid("2eb93c30-c0fa-4e76-96fb-6eff820c4083"), 1, 0, "Sociology", 0, null },
                    { new Guid("af040f08-864b-4e61-8c7a-5408a950d1bf"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("743a5577-d8f5-4074-a261-fc41d73e68f9"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Field", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("e10b638a-97b2-4787-bb1d-24c47efeb9b9"), 0, "Angus", 1 },
                    { new Guid("081f7caf-10b1-484e-bc28-7869ca000bec"), 1, "Kane", 1 },
                    { new Guid("f6623f44-c365-409d-bc51-f4b518c34abb"), 1, "Lian", 2 },
                    { new Guid("7637fd90-2dd2-46ff-9eb0-6c7cb77946eb"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("4e0c6473-b891-49c7-974b-36b5f3fabfd8"), "Freestone" },
                    { new Guid("afa997d6-c151-443c-b81d-c4cddfb4126e"), "Richmont" },
                    { new Guid("f0fb11b0-455f-4cf5-837b-c8a7ae5dcf47"), "Laker" },
                    { new Guid("a27ac51a-769f-4b25-a4de-9c1189ef0a59"), "McCarroll" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Lectures_LectureId",
                table: "Enrollments",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Lectures_LectureId",
                table: "Enrollments");

            migrationBuilder.DropForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments");

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("2eb93c30-c0fa-4e76-96fb-6eff820c4083"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("743a5577-d8f5-4074-a261-fc41d73e68f9"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("af040f08-864b-4e61-8c7a-5408a950d1bf"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("b3a151db-da23-4b3e-bd6d-6ae2f5c17b4e"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("081f7caf-10b1-484e-bc28-7869ca000bec"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("7637fd90-2dd2-46ff-9eb0-6c7cb77946eb"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("e10b638a-97b2-4787-bb1d-24c47efeb9b9"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f6623f44-c365-409d-bc51-f4b518c34abb"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("4e0c6473-b891-49c7-974b-36b5f3fabfd8"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a27ac51a-769f-4b25-a4de-9c1189ef0a59"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("afa997d6-c151-443c-b81d-c4cddfb4126e"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("f0fb11b0-455f-4cf5-837b-c8a7ae5dcf47"));

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<Guid>(
                name: "LectureId",
                table: "Enrollments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "Enrollments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("0a537687-b5af-421e-8ec9-c92d88594e7e"), 2, 0, "Philosophy", 0, null },
                    { new Guid("b5b76767-d0a1-4a87-994f-cc93666b3b35"), 1, 0, "Sociology", 0, null },
                    { new Guid("2b92d281-3584-4495-ab95-57623f6f096a"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("726d1094-b6fb-4158-bcde-47a8458f1a35"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Field", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("f01cca26-c424-4c9c-a610-254569cef6cf"), 0, "Angus", 1 },
                    { new Guid("13ede6f1-a831-4b07-905e-59444ffc81bd"), 1, "Kane", 1 },
                    { new Guid("c3157bdb-dee9-4906-9d96-d25c95b3c5a6"), 1, "Lian", 2 },
                    { new Guid("20f90926-6707-48d2-b010-98a499e81027"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("138332c5-8e5b-47b1-bc49-ea53ad58d43f"), "Freestone" },
                    { new Guid("0995b919-43f7-4b2e-bfe7-a254ca6497e7"), "Richmont" },
                    { new Guid("86274afc-9ccf-4873-8aaf-994491aca101"), "Laker" },
                    { new Guid("623d2cc2-18f4-4397-950d-846a6aac7bdf"), "McCarroll" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Lectures_LectureId",
                table: "Enrollments",
                column: "LectureId",
                principalTable: "Lectures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Enrollments_Students_StudentId",
                table: "Enrollments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
