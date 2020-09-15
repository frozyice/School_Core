using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateLecture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("155f25dc-76c2-46c4-a44f-59d8bace6b90"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("5ccf2c61-fbd2-4c2a-a9d0-53c162738934"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("79219936-9658-4d3f-87a4-fbe30a52b627"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("a4048b5c-9a23-4fe3-b7ad-7c59e9a2056d"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("4cc8323a-aca2-46ff-be78-495001f50742"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("5e871c48-2828-4e96-b141-57f64b51f7ec"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("b2f313b9-25a2-4867-b066-f5046545ba19"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("f612cf08-3726-4ba3-938a-e08f66080f63"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("183a7a3b-e852-4b40-8d10-bbcf0b8a1c97"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("22b87db9-7df9-4e5b-88e6-212c9ffd4233"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("7d914709-f538-4d3e-a520-c2fcaadfcc70"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("f792b751-eb28-4939-a2c2-9b82b3309e2f"));

            migrationBuilder.DropColumn(
                name: "Field",
                table: "Lectures");

            migrationBuilder.AddColumn<int>(
                name: "FieldOfStudy",
                table: "Lectures",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "FieldOfStudy", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("0620ab88-3c61-4e0c-b9f6-65592152e654"), 2, 0, "Philosophy", 1, null },
                    { new Guid("bd6e9c14-2fb1-4c95-ae17-02fd154eb5c6"), 1, 0, "Sociology", 1, null },
                    { new Guid("6312ab50-bc8b-4a0a-bfdb-1df76f12ed15"), 1, 1, "Introduction To Common Law", 1, null },
                    { new Guid("95da1419-918c-4dee-8097-c8e970230e8d"), 2, 1, "Constitutional Law", 1, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Field", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("35042a45-db64-40c0-ab02-e8c482776cbd"), 0, "Angus", 1 },
                    { new Guid("0bbdc190-c1dd-46cb-a19d-45b19f857c84"), 1, "Kane", 1 },
                    { new Guid("669a4220-9c8f-497a-9515-f840cd8eb382"), 1, "Lian", 2 },
                    { new Guid("9dd36932-1458-4165-9f22-8a024de96bb3"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("b35f8d8e-53d7-4a4d-b201-bae3239e9c0c"), "Freestone" },
                    { new Guid("a3c6abbf-f45f-42f1-ad59-6789065a410b"), "Richmont" },
                    { new Guid("a4e3d206-cef5-4c4e-8077-27eb0ba90408"), "Laker" },
                    { new Guid("1d82ce86-6ac2-4b49-9c1f-7752660856d8"), "McCarroll" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("0620ab88-3c61-4e0c-b9f6-65592152e654"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("6312ab50-bc8b-4a0a-bfdb-1df76f12ed15"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("95da1419-918c-4dee-8097-c8e970230e8d"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("bd6e9c14-2fb1-4c95-ae17-02fd154eb5c6"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("0bbdc190-c1dd-46cb-a19d-45b19f857c84"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("35042a45-db64-40c0-ab02-e8c482776cbd"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("669a4220-9c8f-497a-9515-f840cd8eb382"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("9dd36932-1458-4165-9f22-8a024de96bb3"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("1d82ce86-6ac2-4b49-9c1f-7752660856d8"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a3c6abbf-f45f-42f1-ad59-6789065a410b"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("a4e3d206-cef5-4c4e-8077-27eb0ba90408"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b35f8d8e-53d7-4a4d-b201-bae3239e9c0c"));

            migrationBuilder.DropColumn(
                name: "FieldOfStudy",
                table: "Lectures");

            migrationBuilder.AddColumn<int>(
                name: "Field",
                table: "Lectures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "Field", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("5ccf2c61-fbd2-4c2a-a9d0-53c162738934"), 2, 0, "Philosophy", 0, null },
                    { new Guid("155f25dc-76c2-46c4-a44f-59d8bace6b90"), 1, 0, "Sociology", 0, null },
                    { new Guid("a4048b5c-9a23-4fe3-b7ad-7c59e9a2056d"), 1, 1, "Introduction To Common Law", 0, null },
                    { new Guid("79219936-9658-4d3f-87a4-fbe30a52b627"), 2, 1, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Field", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("b2f313b9-25a2-4867-b066-f5046545ba19"), 0, "Angus", 1 },
                    { new Guid("f612cf08-3726-4ba3-938a-e08f66080f63"), 1, "Kane", 1 },
                    { new Guid("4cc8323a-aca2-46ff-be78-495001f50742"), 1, "Lian", 2 },
                    { new Guid("5e871c48-2828-4e96-b141-57f64b51f7ec"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("f792b751-eb28-4939-a2c2-9b82b3309e2f"), "Freestone" },
                    { new Guid("183a7a3b-e852-4b40-8d10-bbcf0b8a1c97"), "Richmont" },
                    { new Guid("7d914709-f538-4d3e-a520-c2fcaadfcc70"), "Laker" },
                    { new Guid("22b87db9-7df9-4e5b-88e6-212c9ffd4233"), "McCarroll" }
                });
        }
    }
}
