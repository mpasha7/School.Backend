using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Lessons.Commands.UpdateLesson;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Lessons.Commands
{
    public class UpdateLessonCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        [Fact]
        public async Task UpdateLessonCommandHandler_SuccessWithoutNumbersShift()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new UpdateLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            var lesson = await _lessonRepo.GetByIdAsync(4, CancellationToken.None, includeReference: "Course");
            var lessonId = lesson.Id;
            var courseId = lesson.CourseId;
            var coachGuid = lesson.Course.CoachGuid;
            var number = lesson.Number;

            string newTitle = "New Lesson Title";
            string newDesc = "New Lesson Description";
            string newVideoLink = "New Lesson VideoLink";
            var lessonsBefore = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);
            int targetLessonId = lessonsBefore.First(les => les.Number == 1).Id;
            int secondLessonId = lessonsBefore.First(les => les.Number == 2).Id;
            int thirdLessonId = lessonsBefore.First(les => les.Number == 3).Id;

            // Act
            await handler.Handle(
                new UpdateLessonCommand
                {
                    Id = lessonId,
                    CourseId = courseId,
                    CoachGuid = coachGuid,
                    Number = number,
                    Title = newTitle,
                    Description = newDesc,
                    VideoLink = newVideoLink
                },
                CancellationToken.None
            );
            var lessonsAfter = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);

            // Assert
            Assert.NotNull(
                await Context.Lessons.SingleOrDefaultAsync(
                    les => les.Id ==lessonId
                        && les.CourseId == courseId
                        && les.Number == number
                        && les.Title == newTitle
                        && les.Description == newDesc
                        && les.VideoLink == newVideoLink));
            Assert.Equal(3, lessonsAfter.Count());
            Assert.Equal(1, lessonsAfter.First(les => les.Id == targetLessonId).Number);
            Assert.Equal(2, lessonsAfter.First(les => les.Id == secondLessonId).Number);
            Assert.Equal(3, lessonsAfter.First(les => les.Id == thirdLessonId).Number);
        }

        [Fact]
        public async Task UpdateLessonCommandHandler_SuccessWithNumbersShift()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new UpdateLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            var lesson = await _lessonRepo.GetByIdAsync(4, CancellationToken.None, includeReference: "Course");
            var lessonId = lesson.Id;
            var courseId = lesson.CourseId;
            var coachGuid = lesson.Course.CoachGuid;

            int newNumber = 2;
            string newTitle = "New Lesson Title";
            string newDesc = "New Lesson Description";
            string newVideoLink = "New Lesson VideoLink";
            var lessonsBefore = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);
            int targetLessonId = lessonsBefore.First(les => les.Number == 1).Id;
            int secondLessonId = lessonsBefore.First(les => les.Number == 2).Id;
            int thirdLessonId = lessonsBefore.First(les => les.Number == 3).Id;

            // Act
            await handler.Handle(
                new UpdateLessonCommand
                {
                    Id = lessonId,
                    CourseId = courseId,
                    CoachGuid = coachGuid,
                    Number = newNumber,
                    Title = newTitle,
                    Description = newDesc,
                    VideoLink = newVideoLink
                },
                CancellationToken.None
            );
            var lessonsAfter = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);

            // Assert
            Assert.NotNull(
                await Context.Lessons.SingleOrDefaultAsync(
                    les => les.Id == lessonId
                        && les.CourseId == courseId
                        && les.Number == newNumber
                        && les.Title == newTitle
                        && les.Description == newDesc
                        && les.VideoLink == newVideoLink));
            Assert.Equal(3, lessonsAfter.Count());
            Assert.Equal(1, lessonsAfter.First(les => les.Id == secondLessonId).Number);
            Assert.Equal(2, lessonsAfter.First(les => les.Id == targetLessonId).Number);
            Assert.Equal(3, lessonsAfter.First(les => les.Id == thirdLessonId).Number);
        }

        [Fact]
        public async Task UpdateLessonCommandHandler_FailOnWrongId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new UpdateLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new UpdateLessonCommand
                    {
                        Id = 20,
                        CourseId = 2,
                        CoachGuid = olgaId,
                        Number = 1,
                        Title = "Title",
                        Description = "Description",
                        VideoLink = "VideoLink"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateLessonCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new UpdateLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new UpdateLessonCommand
                    {
                        Id = 4,
                        CourseId = 10,
                        CoachGuid = olgaId,
                        Number = 1,
                        Title = "Title",
                        Description = "Description",
                        VideoLink = "VideoLink"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task UpdateLessonCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var handler = new UpdateLessonCommandHandler(
                _lessonRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new UpdateLessonCommand
                    {
                        Id = 4,
                        CourseId = 2,
                        CoachGuid = irinaId,
                        Number = 1,
                        Title = "Title",
                        Description = "Description",
                        VideoLink = "VideoLink"
                    },
                    CancellationToken.None));
        }
    }
}
