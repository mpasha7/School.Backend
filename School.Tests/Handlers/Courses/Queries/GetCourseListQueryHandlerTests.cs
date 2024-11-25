using AutoMapper;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Persistence;
using School.Tests.Common;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Handlers.Courses.Queries
{
    [Collection("QueryCollection")]
    public class GetCourseListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        public GetCourseListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async void GetCourseListQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCourseListQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseListQuery
                {
                    CoachGuid = CoursesContextFactory.UserBGuid
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseListVm>();
            result.Courses.Count.ShouldBe(2);
        }
    }
}
