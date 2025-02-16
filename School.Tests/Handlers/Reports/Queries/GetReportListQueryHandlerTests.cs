using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Reports.Queries.GetReportList;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Reports.Queries
{
    [Collection("QueryCollection")]
    public class GetReportListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public GetReportListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetReportListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetReportListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            int courseId = 2;

            // Act
            var result = await handler.Handle(
                new GetReportListQuery
                {
                    CourseId = courseId,
                    CoachGuid = olgaId
                },
                CancellationToken.None);

            // Assert            
            result.ShouldBeOfType<ReportListVm>();

            result.MaxLessonNumber.ShouldBe(3);

            result.Reports.ShouldNotBeNull();
            var reports = result.Reports.ToList();
            reports.Count.ShouldBe(1);
            reports[0].ShouldBeOfType<ReportLookupDto>();
            reports[0].Id.ShouldBe(2);
            reports[0].StudentGuid.ShouldBe(tomId);
            reports[0].StudentName.ShouldBe("Том");
            reports[0].CreatedAt.ShouldBe(DateTime.Today);
            reports[0].LessonId.ShouldBe(5);
            reports[0].LessonNumber.ShouldBe(2);
            reports[0].LessonTitle.ShouldBe("Второй урок");
        }

        [Fact]
        public async Task GetReportListQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetReportListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetReportListQuery
                    {
                        CourseId = 10,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetReportListQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetReportListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetReportListQuery
                    {
                        CourseId = 2,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }
    }
}
