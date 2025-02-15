using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Reports.Commands.CreateReport;
using School.Domain;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Reports.Commands
{
    public class CreateReportCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task CreateReportCommandHandler_Success()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var handler = new CreateReportCommandHandler(
                new ReportRepository(Context),
                new StudentRepository(Context),
                new LessonRepository(Context),
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            string studentName = "Том";
            string text = "New Report Text";
            string photoName = "photo.png";

            FileStream testFileStram = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "test_photo.png"));
            IFormFile formFile = new FormFile(testFileStram, 0, testFileStram.Length, "file", photoName);

            var maxFileId = await Context.Files.MaxAsync(f => f.Id);

            // Act
            var reportId = await handler.Handle(
                new CreateReportCommand
                {
                    StudentGuid = tomId,
                    StudentName = studentName,
                    Text = text,
                    FormFiles = new List<IFormFile> { formFile },
                    LessonId = 5,
                    CourseId = 2
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Reports.SingleOrDefaultAsync(
                    r => r.Id == reportId
                      && r.StudentGuid == tomId
                      && r.StudentName == studentName
                      && r.CreatedAt.Date == DateTime.Today
                      && r.Text == text
                      && r.LessonId == 5));
            Assert.NotNull(
                await Context.Files.SingleOrDefaultAsync(
                    f => f.Id == maxFileId + 1
                      && f.CreatedAt.Date == DateTime.Today
                      && f.FileName == photoName
                      && f.FileSize == testFileStram.Length
                      && f.FileType == FileTypes.Photo
                      && f.FileOwner == FileOwners.Report
                      && f.ReportId == reportId));
        }

        [Fact]
        public async Task CreateReportCommandHandler_FailOnWrongLessonId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var handler = new CreateReportCommandHandler(
                new ReportRepository(Context),
                new StudentRepository(Context),
                new LessonRepository(Context),
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateReportCommand
                    {
                        StudentGuid = tomId,
                        StudentName = "Том",
                        Text = "Text",
                        LessonId = 20,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateReportCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var handler = new CreateReportCommandHandler(
                new ReportRepository(Context),
                new StudentRepository(Context),
                new LessonRepository(Context),
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new CreateReportCommand
                    {
                        StudentGuid = tomId,
                        StudentName = "Том",
                        Text = "Text",
                        LessonId = 5,
                        CourseId = 10
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateReportCommandHandler_FailOnWrongStudentId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var handler = new CreateReportCommandHandler(
                new ReportRepository(Context),
                new StudentRepository(Context),
                new LessonRepository(Context),
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new CreateReportCommand
                    {
                        StudentGuid = alexId,
                        StudentName = "Алекс",
                        Text = "Text",
                        LessonId = 5,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }
    }
}
