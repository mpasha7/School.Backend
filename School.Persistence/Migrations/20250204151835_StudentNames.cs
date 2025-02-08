using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class StudentNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Reports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "Applies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 746, DateTimeKind.Local).AddTicks(8074));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 757, DateTimeKind.Local).AddTicks(5325));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 757, DateTimeKind.Local).AddTicks(5413));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 757, DateTimeKind.Local).AddTicks(5418));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 759, DateTimeKind.Local).AddTicks(9696));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 760, DateTimeKind.Local).AddTicks(4428));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 760, DateTimeKind.Local).AddTicks(4470));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 4, 20, 18, 31, 760, DateTimeKind.Local).AddTicks(4473));

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "Applies");

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 835, DateTimeKind.Local).AddTicks(7817));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 840, DateTimeKind.Local).AddTicks(8969));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 840, DateTimeKind.Local).AddTicks(9028));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 840, DateTimeKind.Local).AddTicks(9037));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 842, DateTimeKind.Local).AddTicks(7221));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3643));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3666));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3669));

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Courses_CourseId",
                table: "Files",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
