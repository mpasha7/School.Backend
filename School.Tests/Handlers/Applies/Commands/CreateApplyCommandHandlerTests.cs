using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Applies.Commands.CreateApply;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Applies.Commands
{
    public class CreateApplyCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task CreateApplyCommandHandler_Success()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateApplyCommandHandler(
                new ApplyRepository(Context),
                _courseRepo
            );

            string studentName = "Алекс";
            int courseId = 1;

            // Act
            var applyId = await handler.Handle(
                new CreateApplyCommand
                {
                    StudentGuid = alexId,
                    StudentName = studentName,
                    CourseId = courseId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Applies.SingleOrDefaultAsync(
                    a => a.Id == applyId
                      && a.StudentGuid == alexId
                      && a.StudentName == studentName
                      && a.IsAssepted == false
                      && a.CourseId == courseId));
        }

        [Fact]
        public async Task CreateApplyCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateApplyCommandHandler(
                new ApplyRepository(Context),
                _courseRepo
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateApplyCommand
                    {
                        StudentGuid = alexId,
                        StudentName = "Алекс",
                        CourseId = 10
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateApplyCommandHandler_FailOnStudentAlreadyHasCourse()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateApplyCommandHandler(
                new ApplyRepository(Context),
                _courseRepo
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ActionAlreadyCompletedException>(async () =>
                await handler.Handle(
                    new CreateApplyCommand
                    {
                        StudentGuid = alexId,
                        StudentName = "Алекс",
                        CourseId = 3
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateApplyCommandHandler_FailOnStudentAlreadyHasApplyToCourse()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateApplyCommandHandler(
                new ApplyRepository(Context),
                _courseRepo
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ActionAlreadyCompletedException>(async () =>
                await handler.Handle(
                    new CreateApplyCommand
                    {
                        StudentGuid = tomId,
                        StudentName = "Том",
                        CourseId = 1
                    },
                    CancellationToken.None));
        }
    }
}
