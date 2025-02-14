using AutoMapper;
using School.Application.Handlers.Courses.Queries.GetCourseList;
using School.Application.Handlers.Courses.Queries.PublicCourseList;
using School.Domain;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Tests.Handlers.Courses.Queries
{
    [Collection("QueryCollection")]
    public class PublicCourseListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        public PublicCourseListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task PublicCourseListQueryHandler_Success()
        {
            // Arrange
            var handler = new PublicCourseListQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            var result = await handler.Handle(
                new PublicCourseListQuery { },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<PublicCourseListVm>();
            result.Courses.Count.ShouldBe(4);
        }
    }
}
