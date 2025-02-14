using AutoMapper;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Domain;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Courses.Queries
{
    [Collection("QueryCollection")]
    public class GetCourseListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public GetCourseListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetCourseListQueryHandler_SuccessForCoach()
        {
            // Arrange
            var handler = new GetCourseListQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseListQuery
                {
                    UserGuid = irinaId,
                    UserRole = UserRoles.Coach
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseListVm>();
            result.Courses.Count.ShouldBe(3);
        }

        [Fact]
        public async Task GetCourseListQueryHandler_SuccessForStudent()
        {
            // Arrange
            var handler = new GetCourseListQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseListQuery
                {
                    UserGuid = tomId,
                    UserRole = UserRoles.Student
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseListVm>();
            result.Courses.Count.ShouldBe(3);
        }

        [Fact]
        public async Task GetCourseListQueryHandler_FailOnWrongUserRole()
        {
            // Arrange
            var handler = new GetCourseListQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new GetCourseListQuery
                    {
                        UserGuid = irinaId,
                        UserRole = UserRoles.Admin
                    },
                    CancellationToken.None));
        }
    }
}
