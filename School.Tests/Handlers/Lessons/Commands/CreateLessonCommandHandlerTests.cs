using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Lessons.Commands.CreateLesson;
using School.Tests.Common;
using School.WebApi.Repository;
using School.WebApi.Services;

namespace School.Tests.Handlers.Lessons.Commands
{
    public class CreateLessonCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        [Fact]
        public async Task CreateLessonCommandHandler_SuccessWithoutNumbersShift()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateLessonCommandHandler(
                _lessonRepo,
                _courseRepo,
                new LessonNumbersService(_lessonRepo)
            );

            var course = await _courseRepo.GetByIdAsync(2, CancellationToken.None, includeCollection: "Lessons");
            var courseId = course.Id;
            var coachGuid = course.CoachGuid;
            var firstLessonId = course.Lessons.First(les => les.Number == 1).Id;
            var secondLessonId = course.Lessons.First(les => les.Number == 2).Id;
            var thirdLessonId = course.Lessons.First(les => les.Number == 3).Id;

            int number = (course.Lessons.Max(les => les.Number) ?? 0) + 1;
            string title = "Added Lesson Title";
            string desc = "Added Lesson Description";
            string vLink = "Added Lesson VideoLink";

            // Act
            var newLessonId = await handler.Handle(
                new CreateLessonCommand
                {
                    CourseId = courseId,
                    CoachGuid = coachGuid,
                    Number = number,
                    Title = title,
                    Description = desc,
                    VideoLink = vLink
                },
                CancellationToken.None
            );

            var lessons = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);

            // Assert
            Assert.NotNull(
                await Context.Lessons.SingleOrDefaultAsync(
                    les => les.Id == newLessonId
                        && les.CourseId == courseId
                        && les.Course.CoachGuid == coachGuid
                        && les.Number == number
                        && les.Title == title
                        && les.Description == desc
                        && les.VideoLink == vLink));
            Assert.Equal(4, lessons.Count());
            Assert.Equal(1, lessons.First(les => les.Id == firstLessonId).Number);
            Assert.Equal(2, lessons.First(les => les.Id == secondLessonId).Number);
            Assert.Equal(3, lessons.First(les => les.Id == thirdLessonId).Number);
            Assert.Equal(4, lessons.First(les => les.Id == newLessonId).Number);
        }

        [Fact]
        public async Task CreateLessonCommandHandler_SuccessWithNumbersShift()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateLessonCommandHandler(
                _lessonRepo,
                _courseRepo,
                new LessonNumbersService(_lessonRepo)
            );

            var course = await _courseRepo.GetByIdAsync(2, CancellationToken.None, includeCollection: "Lessons");
            var courseId = course.Id;
            var coachGuid = course.CoachGuid;
            var firstLessonId = course.Lessons.First(les => les.Number == 1).Id;
            var secondLessonId = course.Lessons.First(les => les.Number == 2).Id;
            var thirdLessonId = course.Lessons.First(les => les.Number == 3).Id;

            int number = 2;
            string title = "Added Lesson Title";
            string desc = "Added Lesson Description";
            string vLink = "Added Lesson VideoLink";

            // Act
            var newLessonId = await handler.Handle(
                new CreateLessonCommand
                {
                    CourseId = courseId,
                    CoachGuid = coachGuid,
                    Number = number,
                    Title = title,
                    Description = desc,
                    VideoLink = vLink
                },
                CancellationToken.None
            );

            var lessons = await _lessonRepo.GetAllAsync(
                CancellationToken.None,
                filter: les => les.CourseId == courseId);

            // Assert
            Assert.NotNull(
                await Context.Lessons.SingleOrDefaultAsync(
                    les => les.Id == newLessonId
                        && les.CourseId == courseId
                        && les.Course.CoachGuid == coachGuid
                        && les.Number == number
                        && les.Title == title
                        && les.Description == desc
                        && les.VideoLink == vLink));
            Assert.Equal(4, lessons.Count());
            Assert.Equal(1, lessons.First(les => les.Id == firstLessonId).Number);
            Assert.Equal(2, lessons.First(les => les.Id == newLessonId).Number);
            Assert.Equal(3, lessons.First(les => les.Id == secondLessonId).Number);
            Assert.Equal(4, lessons.First(les => les.Id == thirdLessonId).Number);
        }

        [Fact]
        public async Task CreateLessonCommandHandler_FailOnWrongId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateLessonCommandHandler(
                _lessonRepo,
                _courseRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateLessonCommand
                    {
                        CourseId = 10,
                        CoachGuid = olgaId,
                        Number = 2,
                        Title = "Title",
                        Description = "Description",
                        VideoLink = "VideoLink"
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateLessonCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var _lessonRepo = new LessonRepository(Context);
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateLessonCommandHandler(
                _lessonRepo,
                _courseRepo,
                new LessonNumbersService(_lessonRepo)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new CreateLessonCommand
                    {
                        CourseId = 2,
                        CoachGuid = irinaId,
                        Number = 2,
                        Title = "Title",
                        Description = "Description",
                        VideoLink = "VideoLink"
                    },
                    CancellationToken.None));
        }
    }
}
