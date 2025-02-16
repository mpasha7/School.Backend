using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Applies.Commands.UpdateApply;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Applies.Commands
{
    public class UpdateApplyCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task UpdateApplyCommandHandler_Success()
        {
            // Arrange
            var _applyRepo = new ApplyRepository(Context);
            var handler = new UpdateApplyCommandHandler(
                _applyRepo,
                new StudentRepository(Context)
            );

            var apply = await _applyRepo.GetByIdAsync(1, CancellationToken.None, includeReference: "Course");
            int applyId = apply.Id;
            int courseId = apply.CourseId;
            string coachGuid = apply.Course.CoachGuid;
            string studentGuid = apply.StudentGuid;

            // Act
            await handler.Handle(
                new UpdateApplyCommand
                {
                    Id = applyId,
                    CourseId = courseId,
                    StudentGuid = studentGuid,
                    CoachGuid = coachGuid
                },
                CancellationToken.None
            );

            // Assert
            Assert.Null(Context.Applies.SingleOrDefault(
                a => a.Id == applyId));
            Assert.NotNull(
                await Context.Students.SingleOrDefaultAsync(
                    s => s.StudentGuid == studentGuid
                      && s.CourseId == courseId));
        }

        [Fact]
        public async Task UpdateApplyCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new UpdateApplyCommandHandler(
                new ApplyRepository(Context),
                new StudentRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateApplyCommand
                    {
                        Id = 10,
                        CourseId = 1,
                        StudentGuid = tomId,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateApplyCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new UpdateApplyCommandHandler(
                new ApplyRepository(Context),
                new StudentRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new UpdateApplyCommand
                    {
                        Id = 1,
                        CourseId = 10,
                        StudentGuid = tomId,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateApplyCommandHandler_FailOnWrongStudentId()
        {
            // Arrange
            var handler = new UpdateApplyCommandHandler(
                new ApplyRepository(Context),
                new StudentRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new UpdateApplyCommand
                    {
                        Id = 1,
                        CourseId = 1,
                        StudentGuid = alexId,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateApplyCommandHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new UpdateApplyCommandHandler(
                new ApplyRepository(Context),
                new StudentRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new UpdateApplyCommand
                    {
                        Id = 1,
                        CourseId = 1,
                        StudentGuid = tomId,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }
    }
}
