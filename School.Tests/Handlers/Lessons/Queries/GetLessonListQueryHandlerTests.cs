using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Lessons.Queries.GetLessonList;
using School.Domain;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Lessons.Queries
{
    [Collection("QueryCollection")]
    public class GetLessonListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
        private readonly string lorem2 = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem";
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        public GetLessonListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetLessonListQueryHandler_SuccessForCoach()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetLessonListQuery
                {
                    CourseId = 2,
                    UserGuid = olgaId,
                    UserRole = UserRoles.Coach
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<LessonListVm>();
            result.Lessons.Count.ShouldBe(3);

            result.Course.ShouldBeOfType<CourseDetailsVm>();
            result.Course.Title.ShouldBe("Йога кундалини");
            result.Course.Description.ShouldBe(lorem2);
            result.Course.ShortDescription.ShouldBe(lorem);
            result.Course.PublicDescription.ShouldBe(lorem2);
            result.Course.BeginQuestionnaire.ShouldBe("");
            result.Course.EndQuestionnaire.ShouldBe("");

            result.MaxLessonNumber.ShouldBe(3);
        }

        [Fact]
        public async Task GetLessonListQueryHandler_SuccessForStudent()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetLessonListQuery
                {
                    CourseId = 2,
                    UserGuid = tomId,
                    UserRole = UserRoles.Student
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<LessonListVm>();
            result.Lessons.Count.ShouldBe(3);

            result.Course.ShouldBeOfType<CourseDetailsVm>();
            result.Course.Title.ShouldBe("Йога кундалини");
            result.Course.Description.ShouldBe(lorem2);
            result.Course.ShortDescription.ShouldBe(lorem);
            result.Course.PublicDescription.ShouldBe(lorem2);
            result.Course.BeginQuestionnaire.ShouldBe("");
            result.Course.EndQuestionnaire.ShouldBe("");

            result.MaxLessonNumber.ShouldBe(3);
        }

        [Fact]
        public async Task GetLessonListQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetLessonListQuery
                    {
                        CourseId = 10,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonListQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetLessonListQuery
                    {
                        CourseId = 2,
                        UserGuid = irinaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonListQueryHandler_FailOnWrongStudentId()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetLessonListQuery
                    {
                        CourseId = 2,
                        UserGuid = alexId,
                        UserRole = UserRoles.Student
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonListQueryHandler_FailOnWrongUserRole()
        {
            // Arrange
            var handler = new GetLessonListQueryHandler(
                new LessonRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new GetLessonListQuery
                    {
                        CourseId = 2,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Admin
                    },
                    CancellationToken.None));
        }
    }
}
