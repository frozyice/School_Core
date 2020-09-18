using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace School_Core.Migrations
{
    public partial class ReworkSeedAndDomainModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    YearOfStudy = table.Column<int>(nullable: false),
                    Field = table.Column<int>(nullable: false)
                }, constraints: table => { table.PrimaryKey("PK_Students", x => x.Id); });

            migrationBuilder.CreateTable(name: "Teachers", columns: table => new {Id = table.Column<Guid>(nullable: false), Name = table.Column<string>(nullable: true)},
                constraints: table => { table.PrimaryKey("PK_Teachers", x => x.Id); });

            migrationBuilder.CreateTable(name: "Lectures",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Field = table.Column<int>(nullable: false),
                    CanTakeFromYear = table.Column<int>(nullable: false),
                    TeacherId = table.Column<Guid>(nullable: true)
                }, constraints: table =>
                {
                    table.PrimaryKey("PK_Lectures", x => x.Id);
                    table.ForeignKey(name: "FK_Lectures_Teachers_TeacherId", column: x => x.TeacherId, principalTable: "Teachers", principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(name: "Enrollment",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StudentId = table.Column<Guid>(nullable: false),
                    LectureId = table.Column<Guid>(nullable: false),
                    Grade = table.Column<int>(nullable: false)
                }, constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollment", x => x.Id);
                    table.ForeignKey(name: "FK_Enrollment_Lectures_LectureId", column: x => x.LectureId, principalTable: "Lectures", principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(name: "FK_Enrollment_Students_StudentId", column: x => x.StudentId, principalTable: "Students", principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(table: "Lectures", columns: new[] {"Id", "CanTakeFromYear", "Field", "Name", "Status", "TeacherId"},
                values: new object[,]
                {
                    {new Guid("3ef9c0a2-d9ed-4b0c-bcda-a980e1d017f5"), 1, 0, "Philosophy", 0, null},
                    {new Guid("b64af4de-aa68-46e2-883e-a9103bd6d276"), 1, 0, "Sociology", 0, null},
                    {new Guid("8e0fff1d-a30b-498e-9284-690c5d0f08ad"), 1, 0, "Introduction To Common Law", 0, null},
                    {new Guid("b55deb5e-a1e2-4d1a-92e1-2dc24459eeb4"), 1, 0, "Constitutional Law", 0, null}
                });

            migrationBuilder.InsertData(table: "Students", columns: new[] {"Id", "Field", "Name", "YearOfStudy"},
                values: new object[,]
                {
                    {new Guid("98dadc27-27a2-47f7-8ef9-79fcba06d36f"), 0, "Angus", 1}, {new Guid("a8c3abf1-5f89-4133-8bc8-8614b8c7e325"), 1, "Kane", 1},
                    {new Guid("3290b43d-a7fb-4fd0-9c03-cff3c82f4e18"), 1, "Lian", 2}, {new Guid("b524cbc0-ecb6-4a40-8536-f95d52607d2d"), 0, "Alissa", 2}
                });

            migrationBuilder.InsertData(table: "Teachers", columns: new[] {"Id", "Name"},
                values: new object[,]
                {
                    {new Guid("c7464cfb-1974-4fd9-a226-78a000f95c74"), "Freestone"}, {new Guid("bbb05fbe-2d66-4997-9a81-337b6a8794bd"), "Richmont"},
                    {new Guid("b2efa763-e1b0-475d-91dc-0dd534049c09"), "Laker"}, {new Guid("5e99e9ab-d812-4d8e-abc9-bb2997aa98a3"), "McCarroll"}
                });

            migrationBuilder.CreateIndex(name: "IX_Enrollment_LectureId", table: "Enrollment", column: "LectureId");

            migrationBuilder.CreateIndex(name: "IX_Enrollment_StudentId", table: "Enrollment", column: "StudentId");

            migrationBuilder.CreateIndex(name: "IX_Lectures_TeacherId", table: "Lectures", column: "TeacherId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Enrollment");

            migrationBuilder.DropTable(name: "Lectures");

            migrationBuilder.DropTable(name: "Students");

            migrationBuilder.DropTable(name: "Teachers");
        }
    }
}