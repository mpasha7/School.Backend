using AutoMapper;
using School.Application.Handlers.Students.Queries.GetStudentsIds;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Students.Queries
{
    [Collection("QueryCollection")]
    public class GetStudentsIdsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public GetStudentsIdsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetStudentsIdsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetStudentsIdsQueryHandler(
                new StudentRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetStudentsIdsQuery
                {
                    CoachGuid = olgaId
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<StudentsIdsVm>();

            result.AllCourses.Count.ShouldBe(1);
            result.AllCourses[0].ShouldBeOfType<StudentCourseLookupDto>();
            result.AllCourses[0].CourseId.ShouldBe(2);
            result.AllCourses[0].Title.ShouldBe("Йога кундалини");

            result.StudentsIds.Count.ShouldBe(1);
            result.StudentsIds[0].ShouldBeOfType<StudentLookupDto>();
            result.StudentsIds[0].StudentGuid.ShouldBe(tomId);
            result.StudentsIds[0].Courses.Count.ShouldBe(1);
            result.StudentsIds[0].Courses[0].ShouldBeOfType<StudentCourseLookupDto>();
            result.StudentsIds[0].Courses[0].CourseId.ShouldBe(2);
            result.StudentsIds[0].Courses[0].Title.ShouldBe("Йога кундалини");
        }
    }
}
