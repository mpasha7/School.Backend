using AutoMapper;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
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
    public class GetCourseDetailsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        public GetCourseDetailsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetCourseDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new GetCourseDetailsQueryHandler(Context, Mapper);

            // Act
            var result = await handler.Handle(
                new GetCourseDetailsQuery
                {
                    Id = 2,
                    CoachGuid = CoursesContextFactory.UserBGuid
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<CourseDetailsVm>();
            result.Title.ShouldBe("Title 2");
            result.Description.ShouldBe("Description 2");
            result.ShortDescription.ShouldBe("ShortDescription 2");
            result.PublicDescription.ShouldBe("PublicDescription 2");
            result.PhotoPath.ShouldBe("PhotoPath 2");
            result.BeginQuestionnaire.ShouldBe("BeginQuestionnaire 2");
            result.EndQuestionnaire.ShouldBe("EndQuestionnaire 2");
        }
    }
}
