using Microsoft.AspNetCore.Hosting;
using Moq;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Commands.DeleteCourse;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Handlers.Courses.Commands
{
    public class DeleteCourseCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteCourseCommandHandler_Success()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new DeleteCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None, includeReference: "Photo");
            if (course == null)
                throw new Exception("Course not found");
            int courseId = course.Id;
            string coachGuid = course.CoachGuid;
            int photoId = course.Photo.Id;

            // Act
            await handler.Handle(
                new DeleteCourseCommand
                {
                    Id = courseId,
                    CoachGuid = coachGuid
                },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Courses.SingleOrDefault(
                c => c.Id == courseId));
            Assert.Null(Context.Files.SingleOrDefault(
                f => f.Id == photoId));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOnWrongId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new DeleteCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCourseCommand
                    {
                        Id = 10,
                        CoachGuid = CoursesContextFactory.TestCoachGuid
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var mockEnv = new Mock<IWebHostEnvironment>();
            mockEnv
                .Setup(m => m.WebRootPath)
                .Returns(Directory.GetCurrentDirectory());
            var _courseRepo = new CourseRepository(Context);

            var handler = new DeleteCourseCommandHandler(
                _courseRepo,
                new FileService(mockEnv.Object, new FileRepository(Context))
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None);
            if (course == null)
                throw new Exception("Course not found");
            int courseId = course.Id;

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new DeleteCourseCommand
                    {
                        Id = courseId,
                        CoachGuid = CoursesContextFactory.TestCoachGuid
                    },
                    CancellationToken.None));
        }

    }
}
