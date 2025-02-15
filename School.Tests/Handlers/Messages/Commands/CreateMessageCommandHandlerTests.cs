using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Messages.Commands.CreateMessage;
using School.Domain;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Messages.Commands
{
    public class CreateMessageCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string irinaId = "3a3b611c-1185-445d-99c5-7f347675ec6e";
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task CreateMessageCommandHandler_SuccessQuestionOfStudent()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateMessageCommandHandler(
                new MessageRepository(Context),
                _courseRepo
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None);
            var courseId = course.Id;
            var courseTitle = course.Title;
            var coachGuid = course.CoachGuid;

            string studentName = "Алекс";
            string text = "New Question Text";

            // Act
            var messageId = await handler.Handle(
                new CreateMessageCommand
                {
                    CourseId = courseId,
                    SenderGuid = alexId,
                    SenderName = studentName,
                    //RecipientGuid = coachGuid,
                    Theme = courseTitle,
                    Text = text,
                    SenrerRole = UserRoles.Student,
                    QuestionId = 0
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Messages.SingleOrDefaultAsync(
                    m => m.Id == messageId
                      && m.CourseId == courseId
                      && m.SenderGuid == alexId
                      && m.SenderName == studentName
                      && m.RecipientGuid == coachGuid
                      && m.CreatedAt.Date == DateTime.Today
                      && m.Theme == courseTitle
                      && m.Text == text
                      && m.QuestionId == null));
        }

        [Fact]
        public async Task CreateMessageCommandHandler_SuccessAnswerOfCoach()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateMessageCommandHandler(
                new MessageRepository(Context),
                _courseRepo
            );

            var course = await _courseRepo.GetByIdAsync(1, CancellationToken.None);
            var courseId = course.Id;
            var courseTitle = course.Title;
            //var coachGuid = course.CoachGuid;

            string coachName = "Ирина";
            string text = "Answer Text";

            // Act
            var messageId = await handler.Handle(
                new CreateMessageCommand
                {
                    CourseId = courseId,
                    SenderGuid = irinaId,
                    SenderName = coachName,
                    RecipientGuid = tomId,
                    Theme = courseTitle,
                    Text = text,
                    SenrerRole = UserRoles.Coach,
                    QuestionId = 1
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Messages.SingleOrDefaultAsync(
                    m => m.Id == messageId
                      && m.CourseId == courseId
                      && m.SenderGuid == irinaId
                      && m.SenderName == coachName
                      && m.RecipientGuid == tomId
                      && m.CreatedAt.Date == DateTime.Today
                      && m.Theme == courseTitle
                      && m.Text == text
                      && m.QuestionId == 1));
        }

        [Fact]
        public async Task CreateMessageCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateMessageCommandHandler(
                new MessageRepository(Context),
                _courseRepo
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new CreateMessageCommand
                    {
                        CourseId = 10,
                        SenderGuid = alexId,
                        SenderName = "Алекс",
                        RecipientGuid = irinaId,
                        Theme = "Theme",
                        Text = "Text",
                        SenrerRole = UserRoles.Student,
                        QuestionId = 0
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task CreateMessageCommandHandler_FailOnEmptySenderName()
        {
            // Arrange
            var _courseRepo = new CourseRepository(Context);
            var handler = new CreateMessageCommandHandler(
                new MessageRepository(Context),
                _courseRepo
            );

            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(
                    new CreateMessageCommand
                    {
                        CourseId = 1,
                        SenderGuid = alexId,
                        SenderName = "",
                        RecipientGuid = irinaId,
                        Theme = "Theme",
                        Text = "Text",
                        SenrerRole = UserRoles.Student,
                        QuestionId = 0
                    },
                    CancellationToken.None));
        }
    }
}
