using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateEnrollment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Enrollment_Lectures_LectureId", table: "Enrollment");

            migrationBuilder.DropForeignKey(name: "FK_Enrollment_Students_StudentId", table: "Enrollment");

            migrationBuilder.DropPrimaryKey(name: "PK_Enrollment", table: "Enrollment");

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("0620ab88-3c61-4e0c-b9f6-65592152e654"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("6312ab50-bc8b-4a0a-bfdb-1df76f12ed15"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("95da1419-918c-4dee-8097-c8e970230e8d"));

            migrationBuilder.DeleteData(table: "Lectures", keyColumn: "Id", keyValue: new Guid("bd6e9c14-2fb1-4c95-ae17-02fd154eb5c6"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("0bbdc190-c1dd-46cb-a19d-45b19f857c84"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("35042a45-db64-40c0-ab02-e8c482776cbd"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("669a4220-9c8f-497a-9515-f840cd8eb382"));

            migrationBuilder.DeleteData(table: "Students", keyColumn: "Id", keyValue: new Guid("9dd36932-1458-4165-9f22-8a024de96bb3"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("1d82ce86-6ac2-4b49-9c1f-7752660856d8"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("a3c6abbf-f45f-42f1-ad59-6789065a410b"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("a4e3d206-cef5-4c4e-8077-27eb0ba90408"));

            migrationBuilder.DeleteData(table: "Teachers", keyColumn: "Id", keyValue: new Guid("b35f8d8e-53d7-4a4d-b201-bae3239e9c0c"));

            migrationBuilder.RenameTable(name: "Enrollment", newName: "Enrollments");

            migrationBuilder.RenameIndex(name: "IX_Enrollment_StudentId", table: "Enrollments", newName: "IX_Enrollments_StudentId");

            migrationBuilder.RenameIndex(name: "IX_Enrollment_LectureId", table: "Enrollments", newName: "IX_Enrollments_LectureId");

            migrationBuilder.AddPrimaryKey(name: "PK_Enrollments", table: "Enrollments", column: "Id");

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

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments", column: "LectureId", principalTable: "Lectures", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments", column: "StudentId", principalTable: "Students", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Lectures_LectureId", table: "Enrollments");

            migrationBuilder.DropForeignKey(name: "FK_Enrollments_Students_StudentId", table: "Enrollments");

            migrationBuilder.DropPrimaryKey(name: "PK_Enrollments", table: "Enrollments");

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

            migrationBuilder.RenameTable(name: "Enrollments", newName: "Enrollment");

            migrationBuilder.RenameIndex(name: "IX_Enrollments_StudentId", table: "Enrollment", newName: "IX_Enrollment_StudentId");

            migrationBuilder.RenameIndex(name: "IX_Enrollments_LectureId", table: "Enrollment", newName: "IX_Enrollment_LectureId");

            migrationBuilder.AddPrimaryKey(name: "PK_Enrollment", table: "Enrollment", column: "Id");

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("0620ab88-3c61-4e0c-b9f6-65592152e654"), 2, 0, "Philosophy", 1, null},
                    {new Guid("bd6e9c14-2fb1-4c95-ae17-02fd154eb5c6"), 1, 0, "Sociology", 1, null},
                    {new Guid("6312ab50-bc8b-4a0a-bfdb-1df76f12ed15"), 1, 1, "Introduction To Common Law", 1, null},
                    {new Guid("95da1419-918c-4dee-8097-c8e970230e8d"), 2, 1, "Constitutional Law", 1, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("35042a45-db64-40c0-ab02-e8c482776cbd"), 0, "Angus", 1}, {new Guid("0bbdc190-c1dd-46cb-a19d-45b19f857c84"), 1, "Kane", 1},
                    {new Guid("669a4220-9c8f-497a-9515-f840cd8eb382"), 1, "Lian", 2}, {new Guid("9dd36932-1458-4165-9f22-8a024de96bb3"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("b35f8d8e-53d7-4a4d-b201-bae3239e9c0c"), "Freestone"}, {new Guid("a3c6abbf-f45f-42f1-ad59-6789065a410b"), "Richmont"},
                    {new Guid("a4e3d206-cef5-4c4e-8077-27eb0ba90408"), "Laker"}, {new Guid("1d82ce86-6ac2-4b49-9c1f-7752660856d8"), "McCarroll"}
                });

            migrationBuilder.AddForeignKey(name: "FK_Enrollment_Lectures_LectureId", table: "Enrollment", column: "LectureId", principalTable: "Lectures", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(name: "FK_Enrollment_Students_StudentId", table: "Enrollment", column: "StudentId", principalTable: "Students", principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}