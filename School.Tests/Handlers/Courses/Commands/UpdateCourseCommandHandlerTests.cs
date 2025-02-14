using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Commands.UpdateCourse;
using School.Domain;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Courses.Commands
{
    public class UpdateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateCourseCommandHandler_Success()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new UpdateCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None, includeReference: "Photo");
            if (course == null)
                throw new Exception("Course not found");
            int courseId = course.Id;
            string coachGuid = course.CoachGuid;
            int photoId = course.Photo.Id;

            string newTitle = "new title";
            string newDesc = "new description";
            string newShortDesc = "new short description";
            string newPublicDesc = "new public description";
            string newBeginQuest = "new begin questionaire";
            string newEndQuest = "new end questionaire";
            string newPhotoName = "photo.png";

            FileStream testFileStram = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "test_photo.png"));
            IFormFile formFile = new FormFile(testFileStram, 0, testFileStram.Length, "file", newPhotoName);

            // Act
            await handler.Handle(
                new UpdateCourseCommand
                {
                    Id = courseId,
                    CoachGuid = coachGuid,

                    Title = newTitle,
                    Description = newDesc,
                    ShortDescription = newShortDesc,
                    PublicDescription = newPublicDesc,
                    BeginQuestionnaire = newBeginQuest,
                    EndQuestionnaire = newEndQuest,

                    FormFile = formFile
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Courses.SingleOrDefaultAsync(
                    c => c.Id == courseId
                      && c.CoachGuid == coachGuid
                      && c.Title == newTitle
                      && c.Description == newDesc
                      && c.ShortDescription == newShortDesc
                      && c.PublicDescription == newPublicDesc
                      && c.BeginQuestionnaire == newBeginQuest
                      && c.EndQuestionnaire == newEndQuest));
            Assert.Null(Context.Files.SingleOrDefault(
                    f => f.Id == photoId));
            Assert.NotNull(
                await Context.Files.SingleOrDefaultAsync(
                    f => f.Id == 5
                      && f.CreatedAt.Date == DateTime.Today
                      && f.FileName == newPhotoName
                      && f.FileSize == testFileStram.Length
                      && f.FileType == FileTypes.Photo
                      && f.FileOwner == FileOwners.Course
                      && f.CourseId == courseId));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOnWrongId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new UpdateCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateCourseCommand
                    {
                        Id = 10,
                        CoachGuid = CoursesContextFactory.TestCoachGuid,
                        Description = "new description"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateCourseCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new UpdateCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None, includeReference: "Photo");
            if (course == null)
                throw new Exception("Course not found");
            int courseId = course.Id;

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new UpdateCourseCommand
                    {
                        Id = courseId,
                        CoachGuid = CoursesContextFactory.TestCoachGuid,
                        Description = "new description"
                    },
                    CancellationToken.None));
        }
    }
}
