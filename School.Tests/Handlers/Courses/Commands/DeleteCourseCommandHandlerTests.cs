using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Commands.DeleteCourse;
using School.Tests.Common;
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
            var handler = new DeleteCourseCommandHandler(Context);

            // Act
            await handler.Handle(
                new DeleteCourseCommand
                {
                    Id = CoursesContextFactory.CourseIdForDelete,
                    CoachGuid = CoursesContextFactory.UserAGuid
                },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Courses.SingleOrDefault(
                c => c.Id == CoursesContextFactory.CourseIdForDelete));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteCourseCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteCourseCommand
                    {
                        Id = 5,
                        CoachGuid = CoursesContextFactory.UserAGuid
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCourseCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var handler = new DeleteCourseCommandHandler(Context);

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new DeleteCourseCommand
                    {
                        Id = CoursesContextFactory.CourseIdForDelete,
                        CoachGuid = CoursesContextFactory.UserBGuid
                    },
                    CancellationToken.None));
        }

    }
}
