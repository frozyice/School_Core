using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class UpdateSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("3ef9c0a2-d9ed-4b0c-bcda-a980e1d017f5"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("8e0fff1d-a30b-498e-9284-690c5d0f08ad"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("b55deb5e-a1e2-4d1a-92e1-2dc24459eeb4"));

            migrationBuilder.DeleteData(
                table: "Lectures",
                keyColumn: "Id",
                keyValue: new Guid("b64af4de-aa68-46e2-883e-a9103bd6d276"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("3290b43d-a7fb-4fd0-9c03-cff3c82f4e18"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("98dadc27-27a2-47f7-8ef9-79fcba06d36f"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("a8c3abf1-5f89-4133-8bc8-8614b8c7e325"));

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: new Guid("b524cbc0-ecb6-4a40-8536-f95d52607d2d"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("5e99e9ab-d812-4d8e-abc9-bb2997aa98a3"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("b2efa763-e1b0-475d-91dc-0dd534049c09"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("bbb05fbe-2d66-4997-9a81-337b6a8794bd"));

            migrationBuilder.DeleteData(
                table: "Teachers",
                keyColumn: "Id",
                keyValue: new Guid("c7464cfb-1974-4fd9-a226-78a000f95c74"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Lectures",
                columns: new[] { "Id", "CanTakeFromYear", "Field", "Name", "Status", "TeacherId" },
                values: new object[,]
                {
                    { new Guid("3ef9c0a2-d9ed-4b0c-bcda-a980e1d017f5"), 1, 0, "Philosophy", 0, null },
                    { new Guid("b64af4de-aa68-46e2-883e-a9103bd6d276"), 1, 0, "Sociology", 0, null },
                    { new Guid("8e0fff1d-a30b-498e-9284-690c5d0f08ad"), 1, 0, "Introduction To Common Law", 0, null },
                    { new Guid("b55deb5e-a1e2-4d1a-92e1-2dc24459eeb4"), 1, 0, "Constitutional Law", 0, null }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Field", "Name", "YearOfStudy" },
                values: new object[,]
                {
                    { new Guid("98dadc27-27a2-47f7-8ef9-79fcba06d36f"), 0, "Angus", 1 },
                    { new Guid("a8c3abf1-5f89-4133-8bc8-8614b8c7e325"), 1, "Kane", 1 },
                    { new Guid("3290b43d-a7fb-4fd0-9c03-cff3c82f4e18"), 1, "Lian", 2 },
                    { new Guid("b524cbc0-ecb6-4a40-8536-f95d52607d2d"), 0, "Alissa", 2 }
                });

            migrationBuilder.InsertData(
                table: "Teachers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("c7464cfb-1974-4fd9-a226-78a000f95c74"), "Freestone" },
                    { new Guid("bbb05fbe-2d66-4997-9a81-337b6a8794bd"), "Richmont" },
                    { new Guid("b2efa763-e1b0-475d-91dc-0dd534049c09"), "Laker" },
                    { new Guid("5e99e9ab-d812-4d8e-abc9-bb2997aa98a3"), "McCarroll" }
                });
        }
    }
}
