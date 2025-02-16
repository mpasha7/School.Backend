using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Feedbacks.Commands.CreateFeedback;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Feedbacks.Commands
{
    public class CreateFeedbackCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        [Fact]
        public async Task CreateFeedbackCommandHandler_Success()
        {
            // Arrange
            var handler = new CreateFeedbackCommandHandler(
                new FeedbackRepository(Context),
                new ReportRepository(Context),
                new LessonRepository(Context)
            );

            string text = "Feedback Text for 2 Report";
            int reportId = 2;
            int lessonId = 5;
            int courseId = 2;

            // Act
            var feedbackId = await handler.Handle(
                new CreateFeedbackCommand
                {
                    CoachGuid = olgaId,
                    Text = text,
                    ReportId = reportId,
                    LessonId = lessonId,
                    CourseId = courseId
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Feedbacks.SingleOrDefaultAsync(
                    fb => fb.Id == feedbackId
                       && fb.CreatedAt.Date == DateTime.Today
                       && fb.Text == text
                       && fb.ReportId == reportId));
        }

        [Fact]
        public async Task CreateFeedbackCommandHandler_FailOnWrongReportId()
        {
            // Arrange
            var handler = new CreateFeedbackCommandHandler(
                new FeedbackRepository(Context),
                new ReportRepository(Context),
                new LessonRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateFeedbackCommand
                    {
                        CoachGuid = olgaId,
                        Text = "Text",
                        ReportId = 10,
                        LessonId = 5,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateFeedbackCommandHandler_FailOnWrongLessonId()
        {
            // Arrange
            var handler = new CreateFeedbackCommandHandler(
                new FeedbackRepository(Context),
                new ReportRepository(Context),
                new LessonRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateFeedbackCommand
                    {
                        CoachGuid = olgaId,
                        Text = "Text",
                        ReportId = 2,
                        LessonId = 20,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateFeedbackCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new CreateFeedbackCommandHandler(
                new FeedbackRepository(Context),
                new ReportRepository(Context),
                new LessonRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new CreateFeedbackCommand
                    {
                        CoachGuid = olgaId,
                        Text = "Text",
                        ReportId = 2,
                        LessonId = 5,
                        CourseId = 10
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateFeedbackCommandHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new CreateFeedbackCommandHandler(
                new FeedbackRepository(Context),
                new ReportRepository(Context),
                new LessonRepository(Context)
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new CreateFeedbackCommand
                    {
                        CoachGuid = irinaId,
                        Text = "Text",
                        ReportId = 2,
                        LessonId = 5,
                        CourseId = 2
                    },
                    CancellationToken.None));
        }
    }
}
