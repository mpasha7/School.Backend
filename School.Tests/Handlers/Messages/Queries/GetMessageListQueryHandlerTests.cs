using AutoMapper;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Messages.Queries.GetMessageList;
using School.Domain;
using School.Persistence;
using School.Tests.Common;
using School.WebApi.Repository;
using Shouldly;

namespace School.Tests.Handlers.Messages.Queries
{
    [Collection("QueryCollection")]
    public class GetMessageListQueryHandlerTests
    {
        private readonly SchoolDbContext Context;
        private readonly IMapper Mapper;

        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string olgaId = "0b869d28-ab51-4310-ba0c-a3934e1de6de";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";

        public GetMessageListQueryHandlerTests(TestQueriesFixtire fixtire)
        {
            Context = fixtire.Context;
            Mapper = fixtire.Mapper;
        }

        [Fact]
        public async Task GetMessageListQueryHandler_SuccessForStudent()
        {
            // Arrange
            var handler = new GetMessageListQueryHandler(
                new MessageRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetMessageListQuery
                {
                    CourseId = 1,
                    UserGuid = tomId,
                    UserRole = UserRoles.Student
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<MessageListVm>();

            result.Messages.Count.ShouldBe(1);
            result.Messages[0].ShouldBeOfType<MessageLookupDto>();
            result.Messages[0].Id.ShouldBe(1);
            result.Messages[0].SenderGuid.ShouldBe(tomId);
            result.Messages[0].SenderName.ShouldBe("Том");
            result.Messages[0].RecipientGuid.ShouldBe(irinaId);
            result.Messages[0].Theme.ShouldBe("Гимнастика на шею");
            result.Messages[0].Text.ShouldBe("Question Text");

            result.Messages[0].Answers.ShouldNotBeNull();
            var answers = result.Messages[0].Answers.ToList();
            answers.Count.ShouldBe(1);
            answers[0].Id.ShouldBe(2);
            answers[0].SenderGuid.ShouldBe(irinaId);
            answers[0].SenderName.ShouldBe("Ирина");
            answers[0].RecipientGuid.ShouldBe(tomId);
            answers[0].Theme.ShouldBe("Гимнастика на шею");
            answers[0].Text.ShouldBe("Answer Text");
        }

        [Fact]
        public async Task GetMessageListQueryHandler_SuccessForCoach()
        {
            // Arrange
            var handler = new GetMessageListQueryHandler(
                new MessageRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            var result = await handler.Handle(
                new GetMessageListQuery
                {
                    CourseId = 1,
                    UserGuid = irinaId,
                    UserRole = UserRoles.Coach
                },
                CancellationToken.None);

            // Assert
            result.ShouldBeOfType<MessageListVm>();

            result.Messages.Count.ShouldBe(1);
            result.Messages[0].ShouldBeOfType<MessageLookupDto>();
            result.Messages[0].Id.ShouldBe(1);
            result.Messages[0].SenderGuid.ShouldBe(tomId);
            result.Messages[0].SenderName.ShouldBe("Том");
            result.Messages[0].RecipientGuid.ShouldBe(irinaId);
            result.Messages[0].Theme.ShouldBe("Гимнастика на шею");
            result.Messages[0].Text.ShouldBe("Question Text");

            result.Messages[0].Answers.ShouldNotBeNull();
            var answers = result.Messages[0].Answers.ToList();
            answers.Count.ShouldBe(1);
            answers[0].Id.ShouldBe(2);
            answers[0].SenderGuid.ShouldBe(irinaId);
            answers[0].SenderName.ShouldBe("Ирина");
            answers[0].RecipientGuid.ShouldBe(tomId);
            answers[0].Theme.ShouldBe("Гимнастика на шею");
            answers[0].Text.ShouldBe("Answer Text");
        }

        [Fact]
        public async Task GetMessageListQueryHandler_FailOnWrongCourseId()
        {
            // Arrange
            var handler = new GetMessageListQueryHandler(
                new MessageRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new GetMessageListQuery
                    {
                        CourseId = 10,
                        UserGuid = irinaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetMessageListQueryHandler_FailOnWrongCoachId()
        {
            // Arrange
            var handler = new GetMessageListQueryHandler(
                new MessageRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new GetMessageListQuery
                    {
                        CourseId = 1,
                        UserGuid = olgaId,
                        UserRole = UserRoles.Coach
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task GetMessageListQueryHandler_FailOnWrongUserRole()
        {
            // Arrange
            var handler = new GetMessageListQueryHandler(
                new MessageRepository(Context),
                new CourseRepository(Context),
                Mapper
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new GetMessageListQuery
                    {
                        CourseId = 1,
                        UserGuid = irinaId,
                        UserRole = UserRoles.Admin
                    },
                    CancellationToken.None));
        }
    }
}
