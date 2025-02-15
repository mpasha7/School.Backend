using Microsoft.EntityFrameworkCore;
using School.Application.Common.Exceptions;
using School.Application.Handlers.Messages.Commands.DeleteMessage;
using School.Tests.Common;
using School.WebApi.Repository;

namespace School.Tests.Handlers.Messages.Commands
{
    public class DeleteMessageCommandHandlerTests : TestCommandBase
    {
        // Course Data from DataManager.SeedDatabase() method
        private readonly string tomId = "77be0187-1d57-42dd-8d76-145c36c51bed";
        private readonly string alexId = "acc53bf2-c3f6-442b-99c0-da2cf971516e";

        [Fact]
        public async Task DeleteMessageCommandHandler_SuccessQuestionOfStudent()
        {
            // Arrange
            var _messageRepo = new MessageRepository(Context);
            var handler = new DeleteMessageCommandHandler(_messageRepo);

            var question = await _messageRepo.GetByIdAsync(1, CancellationToken.None);
            var questionId = question.Id;
            var courseId = question.CourseId;
            var senderGuid = question.SenderGuid;
            var answerId = (await _messageRepo.GetByIdAsync(questionId, CancellationToken.None)).Id;

            // Act
            await handler.Handle(
                new DeleteMessageCommand
                {
                    Id = questionId,
                    CourseId = courseId,
                    SenderGuid = senderGuid
                },
                CancellationToken.None);

            // Assert
            Assert.Null(Context.Messages.SingleOrDefault(
                m => m.Id == questionId));
            Assert.Null(Context.Messages.SingleOrDefault(
                m => m.Id == answerId));
        }

        [Fact]
        public async Task DeleteMessageCommandHandler_SuccessAnswerOfCoach()
        {
            // Arrange
            var _messageRepo = new MessageRepository(Context);
            var handler = new DeleteMessageCommandHandler(_messageRepo);

            var answer = await _messageRepo.GetByIdAsync(2, CancellationToken.None);
            var answerId = answer.Id;
            var courseId = answer.CourseId;
            var senderGuid = answer.SenderGuid;
            var questionId = answer.QuestionId;

            // Act
            await handler.Handle(
                new DeleteMessageCommand
                {
                    Id = answerId,
                    CourseId = courseId,
                    SenderGuid = senderGuid
                },
                CancellationToken.None);

            // Assert
            Assert.NotNull(
                await Context.Messages.SingleOrDefaultAsync(
                    m => m.Id == questionId));
            Assert.Null(Context.Messages.SingleOrDefault(
                m => m.Id == answerId));
        }

        [Fact]
        public async Task DeleteMessageCommandHandler_FailOnWrongId()
        {
            // Arrange
            var _messageRepo = new MessageRepository(Context);
            var handler = new DeleteMessageCommandHandler(_messageRepo);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await handler.Handle(
                    new DeleteMessageCommand
                    {
                        Id = 10,
                        CourseId = 1,
                        SenderGuid = tomId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteMessageCommandHandler_FailOnWrongCourseId()
        {
            // Arrange
            var _messageRepo = new MessageRepository(Context);
            var handler = new DeleteMessageCommandHandler(_messageRepo);

            // Act
            // Assert
            await Assert.ThrowsAsync<NotContainsException>(async () =>
                await handler.Handle(
                    new DeleteMessageCommand
                    {
                        Id = 1,
                        CourseId = 10,
                        SenderGuid = tomId
                    },
                    CancellationToken.None));
        }

        [Fact]
        public async Task DeleteMessageCommandHandler_FailOnWrongUserId()
        {
            // Arrange
            var _messageRepo = new MessageRepository(Context);
            var handler = new DeleteMessageCommandHandler(_messageRepo);

            // Act
            // Assert
            await Assert.ThrowsAsync<NoAccessException>(async () =>
                await handler.Handle(
                    new DeleteMessageCommand
                    {
                        Id = 1,
                        CourseId = 1,
                        SenderGuid = alexId
                    },
                    CancellationToken.None));
        }
    }
}
