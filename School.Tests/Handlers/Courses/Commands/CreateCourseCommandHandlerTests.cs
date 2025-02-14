using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Moq;
using School.Application.Handlers.Courses.Commands.CreateCourse;
using School.Domain;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Courses.Commands
{
    public class CreateCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateCourseCommandHandler_Success()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var handler = new CreateCourseCommandHandler(
                new CourseRepository(Context),
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            string title = "course title";
            string desc = "course description";
            string shDesc = "course short description";
            string pubDesc = "course public description";
            string beginQuest = "course begin questionaire";
            string endQuest = "course end questionaire";
            string photoName = "photo.png";

            FileStream testFileStram = File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "test_photo.png"));
            IFormFile formFile = new FormFile(testFileStram, 0, testFileStram.Length, "file", photoName);

            // Act
            var courseId = await handler.Handle(
                new CreateCourseCommand
                {
                    CoachGuid = CoursesContextFactory.TestCoachGuid,

                    Title = title,
                    Description = desc,
                    ShortDescription = shDesc,
                    PublicDescription = pubDesc,
                    BeginQuestionnaire = beginQuest,
                    EndQuestionnaire = endQuest,

                    FormFile = formFile
                },
                CancellationToken.None
            );

            // Assert
            Assert.NotNull(
                await Context.Courses.SingleOrDefaultAsync(
                    c => c.Id == courseId
                      && c.CoachGuid == CoursesContextFactory.TestCoachGuid
                      && c.CreatedDate.Date == DateTime.Today
                      && c.Title == title
                      && c.Description == desc
                      && c.ShortDescription == shDesc
                      && c.PublicDescription == pubDesc
                      && c.BeginQuestionnaire == beginQuest
                      && c.EndQuestionnaire == endQuest));
            Assert.NotNull(
                await Context.Files.SingleOrDefaultAsync(
                    f => f.Id == 5
                      && f.CreatedAt.Date == DateTime.Today
                      && f.FileName == photoName
                      && f.FileSize == testFileStram.Length
                      && f.FileType == FileTypes.Photo
                      && f.FileOwner == FileOwners.Course
                      && f.CourseId == courseId));
        }


    }
}
