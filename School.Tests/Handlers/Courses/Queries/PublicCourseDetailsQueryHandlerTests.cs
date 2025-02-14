using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Comments.Queries.GetCommentList;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Courses.Queries.PublicCourseDetails;
using School.Application.Handlers.Files;
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
    public class PublicCourseDetailsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string lorem2 = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem";

        public PublicCourseDetailsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task PublicCourseDetailsQueryHandler_Success()
        {
            // Arrange
            var handler = new PublicCourseDetailsQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            var result = await handler.Handle(
                new PublicCourseDetailsQuery
                {
                    Id = 2
                },
                CancellationToken.None);

            // Assert            
            result.ShouldBeOfType<PublicCourseDetailsVm>();
            result.Title.ShouldBe("Йога кундалини");
            result.PublicDescription.ShouldBe(lorem2);

            result.Photo.ShouldBeOfType<FileLookupDto>();
            result.Photo.UniqueFileName.ShouldEndWith("Йога кундалини.jpg");
            
            result.Comments.Count.ShouldBe(1);
            result.Comments[0].ShouldBeOfType<CommentLookupDto>();
            result.Comments[0].Id.ShouldBe(1);
            result.Comments[0].StudentName.ShouldBe("Том");
            result.Comments[0].CreatedAt.ShouldBe(DateTime.Today);
            result.Comments[0].Text.ShouldBe("Comment Text");
            result.Comments[0].IsPublic.ShouldBeTrue();
        }

        [Fact]
        public async Task PublicCourseDetailsQueryHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new PublicCourseDetailsQueryHandler(new CourseRepository(Context), Mapper);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new PublicCourseDetailsQuery
                    {
                        Id = 10
                    },
                    CancellationToken.None));
        }
    }
}
