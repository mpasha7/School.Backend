using School.Application.Common.Exceptions;
using School.Application.Handlers.Applies.Commands.DeleteApply;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Applies.Commands
{
    public class DeleteApplyCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        [Fact]
        public async Task DeleteApplyCommandHandler_Success()
        {
            // Arrange
            var _applyRepo = new ApplyRepository(Context);
            var handler = new DeleteApplyCommandHandler(
                _applyRepo
            );

            var apply = await _applyRepo.GetByIdAsync(1, CancellationToken.None, includeReference: "Course");
            int applyId = apply.Id;
            int courseId = apply.CourseId;
            string coachGuid = apply.Course.CoachGuid;
            string studentGuid = apply.StudentGuid;

            // Act
            await handler.Handle(
                new DeleteApplyCommand
                {
                    Id = applyId,
                    CourseId = courseId,
                    CoachGuid = coachGuid
                },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Applies.SingleOrDefault(
                a => a.Id == applyId));
            Assert.Null(Context.Students.SingleOrDefault(
                s => s.StudentGuid == studentGuid
                  && s.CourseId ==courseId));
        }

        [Fact]
        public async Task DeleteApplyCommandHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new DeleteApplyCommandHandler(
                new ApplyRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteApplyCommand
                    {
                        Id = 10,
                        CourseId = 1,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteApplyCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new DeleteApplyCommandHandler(
                new ApplyRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new DeleteApplyCommand
                    {
                        Id = 1,
                        CourseId = 10,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteApplyCommandHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new DeleteApplyCommandHandler(
                new ApplyRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new DeleteApplyCommand
                    {
                        Id = 1,
                        CourseId = 1,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }
    }
}
