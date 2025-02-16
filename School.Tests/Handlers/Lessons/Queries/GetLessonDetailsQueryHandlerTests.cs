using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Courses.Queries.GetCourseDetails;
using School.Application.Handlers.Lessons.Queries.GetLessonDetails;
using School.Application.Handlers.Reports.Queries.GetReportDetails;
using School.Domain;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Lessons.Queries
{
    [Collection("QueryCollection")]
    public class GetLessonDetailsQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";
        private readonly string lorem1 = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
        private readonly string lorem2 = "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem";
        private readonly string videoLink = @"https://vk.com/video_ext.php?oid=54023064&id=456239088&hd=2&hash=ec3da24f3c3555ea";
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        public GetLessonDetailsQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_SuccessForCoach()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            int lessonId = 4;
            int courseId = 2;

            // Act
            var result = await handler.Handle(
                new GetLessonDetailsQuery
                {
                    Id = lessonId,
                    CourseId = courseId,
                    UserGuid = olgaId,
                    UserRole = UserRoles.Coach
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<LessonDetailsVm>();
            result.Number.ShouldBe(1);
            result.Title.ShouldBe("Первый урок");
            result.Description.ShouldBe(lorem1);
            result.VideoLink.ShouldBe(videoLink);

            result.Course.ShouldBeOfType<CourseDetailsVm>();
            result.Course.Id.ShouldBe(2);
            result.Course.Title.ShouldBe("Йога кундалини");
            result.Course.Description.ShouldBe(lorem2);
            result.Course.ShortDescription.ShouldBe(lorem);
            result.Course.PublicDescription.ShouldBe(lorem2);
            result.Course.BeginQuestionnaire.ShouldBe("");
            result.Course.EndQuestionnaire.ShouldBe("");

            result.Report.ShouldBeNull();
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_SuccessForStudent()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetLessonDetailsQuery
                {
                    Id = 4,
                    CourseId = 2,
                    UserGuid = tomId,
                    UserRole = UserRoles.Student
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<LessonDetailsVm>();
            result.Number.ShouldBe(1);
            result.Title.ShouldBe("Первый урок");
            result.Description.ShouldBe(lorem1);
            result.VideoLink.ShouldBe(videoLink);

            result.Course.ShouldBeOfType<CourseDetailsVm>();
            result.Course.Id.ShouldBe(2);
            result.Course.Title.ShouldBe("Йога кундалини");
            result.Course.Description.ShouldBe(lorem2);
            result.Course.ShortDescription.ShouldBe(lorem);
            result.Course.PublicDescription.ShouldBe(lorem2);
            result.Course.BeginQuestionnaire.ShouldBe("");
            result.Course.EndQuestionnaire.ShouldBe("");

            result.Report.ShouldNotBeNull();
            result.Report.ShouldBeOfType<ReportDetailsVm>();
            result.Report.Id.ShouldBe(1);
            result.Report.Text.ShouldBe("Report Text for 4 Lesson");
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_FailOnWrongId()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetLessonDetailsQuery
                    {
                        Id = 20,
                        CourseId = 2,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new GetLessonDetailsQuery
                    {
                        Id = 4,
                        CourseId = 10,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetLessonDetailsQuery
                    {
                        Id = 4,
                        CourseId = 2,
                        UserGuid = irinaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_FailOnWrongStudentId()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetLessonDetailsQuery
                    {
                        Id = 4,
                        CourseId = 2,
                        UserGuid = alexId,
                        UserRole = UserRoles.Student
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetLessonDetailsQueryHandler_FailOnWrongUserRole()
        {
            // Arrange
            var handler = new GetLessonDetailsQueryHandler(
                new LessonRepository(Context),
                new StudentRepository(Context),
                new ReportRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new GetLessonDetailsQuery
                    {
                        Id = 4,
                        CourseId = 2,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Admin
                    },
                    CancellationToken.None));
        }
    }
}
