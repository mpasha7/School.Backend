using School.Application.Common.Exceptions;
using School.Application.Handlers.Students.Commands.RemoveStudentFromCourse;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Students.Commands
{
    public class RemoveStudentFromCourseCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task RemoveStudentFromCourseCommandHandler_Success()
        {
            // Arrange
            var handler = new RemoveStudentFromCourseCommandHandler(
                new StudentRepository(Context),
                new CourseRepository(Context)
            );

            // Act
            await handler.Handle(
                new RemoveStudentFromCourseCommand
                {
                    CoachGuid = olgaId,
                    StudentGuid = tomId,
                    CourseId = 2
                },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Students.SingleOrDefault(
                s => s.CourseId == 2
                  && s.StudentGuid == tomId));
        }

        [Fact]
        public async Task RemoveStudentFromCourseCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new RemoveStudentFromCourseCommandHandler(
                new StudentRepository(Context),
                new CourseRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new RemoveStudentFromCourseCommand
                    {
                        CoachGuid = olgaId,
                        StudentGuid = tomId,
                        CourseId = 10
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task RemoveStudentFromCourseCommandHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new RemoveStudentFromCourseCommandHandler(
                new StudentRepository(Context),
                new CourseRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new RemoveStudentFromCourseCommand
                    {
                        CoachGuid = irinaId,
                        StudentGuid = tomId,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task RemoveStudentFromCourseCommandHandler_FailOnAlreadyActionCompleted()
        {
            // Arrange
            var handler = new RemoveStudentFromCourseCommandHandler(
                new StudentRepository(Context),
                new CourseRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ActionAlreadyCompletedException>(async () =>
                await handler.Handle(
                    new RemoveStudentFromCourseCommand
                    {
                        CoachGuid = olgaId,
                        StudentGuid = alexId,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }
    }
}
