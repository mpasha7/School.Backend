using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Feedbacks.Queries.GetFeedbackDetails;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Feedbacks.Queries
{
    [Collection("QueryCollection")]
    public class GetFeedbackDetailsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        public GetFeedbackDetailsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetFeedbackDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetFeedbackDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                new StudentRepository(Context),
                Mapper
            );

            int reportId = 1;
            int lessonId = 4;
            int courseId = 2;

            // Act
            var result = await handler.Handle(
                new GetFeedbackDetailsQuery
                {
                    ReportId = reportId,
                    LessonId = lessonId,
                    CourseId = courseId,
                    StudentGuid = tomId
                },
                CancellationToken.None);

            result.ShouldBeOfType<FeedbackDetailsVm>();
            result.Id.ShouldBe(1);
            result.Text.ShouldBe("Feedback Text for 1 Report");
            result.ReportId.ShouldBe(reportId);
        }

        [Fact]
        public async Task GetFeedbackDetailsQueryHandler_FailOnWrongReportId()
        {
            // Arrange
            var handler = new GetFeedbackDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                new StudentRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetFeedbackDetailsQuery
                    {
                        ReportId = 10,
                        LessonId = 4,
                        CourseId = 2,
                        StudentGuid = tomId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetFeedbackDetailsQueryHandler_FailOnWrongLessonId()
        {
            // Arrange
            var handler = new GetFeedbackDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                new StudentRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetFeedbackDetailsQuery
                    {
                        ReportId = 1,
                        LessonId = 20,
                        CourseId = 2,
                        StudentGuid = tomId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetFeedbackDetailsQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetFeedbackDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                new StudentRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new GetFeedbackDetailsQuery
                    {
                        ReportId = 1,
                        LessonId = 4,
                        CourseId = 10,
                        StudentGuid = tomId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetFeedbackDetailsQueryHandler_FailOnWrongStudentId()
        {
            // Arrange
            var handler = new GetFeedbackDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                new StudentRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetFeedbackDetailsQuery
                    {
                        ReportId = 1,
                        LessonId = 4,
                        CourseId = 2,
                        StudentGuid = alexId
                    },
                    CancellationToken.None));
        }
    }
}
