using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Files;
using School.Application.Handlers.Reports.Queries.GetReportDetails;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Reports.Queries
{
    [Collection("QueryCollection")]
    public class GetReportDetailsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";

        public GetReportDetailsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetReportDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetReportDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            int reportId = 1;
            int lessonId = 4;
            int courseId = 2;

            // Act
            var result = await handler.Handle(
                new GetReportDetailsQuery
                {
                    Id = reportId,
                    LessonId = lessonId,
                    CourseId = courseId,
                    CoachGuid = olgaId
                },
                CancellationToken.None);

            // Assert            
            result.ShouldBeOfType<ReportDetailsVm>();
            result.Id.ShouldBe(1);
            result.Text.ShouldBe("Report Text for 4 Lesson");

            result.Photos.ShouldNotBeNull();
            var photos = result.Photos.ToList();
            photos.Count.ShouldBe(1);
            photos[0].ShouldBeOfType<FileLookupDto>();
            photos[0].UniqueFileName.ShouldBe("ReportPhoto.png");
        }

        [Fact]
        public async Task GetReportDetailsQueryHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new GetReportDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetReportDetailsQuery
                    {
                        Id = 10,
                        LessonId = 4,
                        CourseId = 2,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetReportDetailsQueryHandler_FailOnWrongLessonId()
        {
            // Arrange
            var handler = new GetReportDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetReportDetailsQuery
                    {
                        Id = 1,
                        LessonId = 20,
                        CourseId = 2,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetReportDetailsQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetReportDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new GetReportDetailsQuery
                    {
                        Id = 1,
                        LessonId = 4,
                        CourseId = 10,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetReportDetailsQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetReportDetailsQueryHandler(
                new LessonRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetReportDetailsQuery
                    {
                        Id = 1,
                        LessonId = 4,
                        CourseId = 2,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }
    }
}
