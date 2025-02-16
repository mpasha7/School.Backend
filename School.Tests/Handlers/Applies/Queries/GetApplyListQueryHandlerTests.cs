using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Applies.Queries.GetApplyList;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Applies.Queries
{
    [Collection("QueryCollection")]
    public class GetApplyListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public GetApplyListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetApplyListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetApplyListQueryHandler(
                new ApplyRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetApplyListQuery
                {
                    CourseId = 1,
                    CoachGuid = irinaId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<ApplyListVm>();

            result.Applies.Count.ShouldBe(1);
            result.Applies[0].ShouldBeOfType<ApplyLookupDto>();
            result.Applies[0].Id.ShouldBe(1);
            result.Applies[0].StudentGuid.ShouldBe(tomId);
            result.Applies[0].StudentName.ShouldBe("Том");
        }

        [Fact]
        public async Task GetApplyListQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetApplyListQueryHandler(
                new ApplyRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetApplyListQuery
                    {
                        CourseId = 10,
                        CoachGuid = irinaId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetApplyListQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetApplyListQueryHandler(
                new ApplyRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetApplyListQuery
                    {
                        CourseId = 1,
                        CoachGuid = olgaId
                    },
                    CancellationToken.None));
        }
    }
}
