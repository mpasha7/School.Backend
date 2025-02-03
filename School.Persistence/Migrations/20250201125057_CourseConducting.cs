using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CourseConducting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Files_CourseId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Files",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "LessonId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Applies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAssepted = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applies_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assessments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assessments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assessments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecipientGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    SenredRole = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentGuid = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReportId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                columns: new[] { "CreatedAt", "LessonId", "ReportId" },
                values: new object[] { new DateTime(2025, 2, 1, 17, 50, 53, 842, DateTimeKind.Local).AddTicks(7221), null, null });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "LessonId", "ReportId" },
                values: new object[] { new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3643), null, null });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "LessonId", "ReportId" },
                values: new object[] { new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3666), null, null });

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "LessonId", "ReportId" },
                values: new object[] { new DateTime(2025, 2, 1, 17, 50, 53, 843, DateTimeKind.Local).AddTicks(3669), null, null });

            migrationBuilder.CreateIndex(
                name: "IX_Files_CourseId",
                table: "Files",
                column: "CourseId",
                unique: true,
                filter: "[CourseId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Files_LessonId",
                table: "Files",
                column: "LessonId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_ReportId",
                table: "Files",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Applies_CourseId",
                table: "Applies",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Assessments_CourseId",
                table: "Assessments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CourseId",
                table: "Comments",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_ReportId",
                table: "Feedbacks",
                column: "ReportId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CourseId",
                table: "Messages",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_Id",
                table: "Reports",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reports_LessonId",
                table: "Reports",
                column: "LessonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files",
                column: "ReportId",
                principalTable: "Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Lessons_LessonId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Reports_ReportId",
                table: "Files");

            migrationBuilder.DropTable(
                name: "Applies");

            migrationBuilder.DropTable(
                name: "Assessments");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropIndex(
                name: "IX_Files_CourseId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_LessonId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_ReportId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "LessonId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "CourseId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 383, DateTimeKind.Local).AddTicks(7282));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5056));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5136));

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedDate",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 390, DateTimeKind.Local).AddTicks(5140));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(5795));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9676));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9688));

            migrationBuilder.UpdateData(
                table: "Files",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 1, 19, 21, 9, 29, 392, DateTimeKind.Local).AddTicks(9691));

            migrationBuilder.CreateIndex(
                name: "IX_Files_CourseId",
                table: "Files",
                column: "CourseId",
                unique: true);
        }
    }
}
