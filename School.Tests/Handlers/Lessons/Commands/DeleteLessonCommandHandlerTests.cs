using School.Application.Common.Exceptions;
using School.Application.Handlers.Lessons.Commands.DeleteLesson;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Lessons.Commands
{
    public class DeleteLessonCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        [Fact]
        public async Task DeleteLessonCommandHandler_Success()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new DeleteLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            var lesson = await _lessonRepo.GetByIdAsync(4, CancellationToken.None, includeReference: "Course");
            if (lesson == null)
                throw new Exception("Lesson not found");
            var lessonId = lesson.Id;
            var courseId = lesson.CourseId;
            var coachGuid = lesson.Course.CoachGuid;

            // Act
            await handler.Handle(
                new DeleteLessonCommand
                {
                    Id = lessonId,
                    CourseId = courseId,
                    CoachGuid = coachGuid
                },
                CancellationToken.None);

            var lessons = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId ==courseId);

            // Assert
            Assert.Null(Context.Lessons.SingleOrDefault(
                les => les.Id ==lessonId));
            Assert.Equal(2, lessons.Count());
            Assert.Equal(1, lessons.First(les => les.Id == 5).Number);
            Assert.Equal(2, lessons.First(les => les.Id == 6).Number);
        }

        [Fact]
        public async Task DeleteLessonCommandHandler_FailOnWrongId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new DeleteLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteLessonCommand
                    {
                        Id = 20,
                        CourseId = 2,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteLessonCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new DeleteLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new DeleteLessonCommand
                    {
                        Id = 4,
                        CourseId = 10,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteLessonCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new DeleteLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new DeleteLessonCommand
                    {
                        Id = 4,
                        CourseId = 2,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }
    }
}
