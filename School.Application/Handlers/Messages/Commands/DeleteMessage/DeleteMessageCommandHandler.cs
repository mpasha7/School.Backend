using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
    {
        private readonly IMessageRepository _messageRepository;

        public DeleteMessageCommandHandler(IMessageRepository messageRepository)
        {
            this._messageRepository = messageRepository;
        }

        public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
        {
            var message = await _messageRepository.GetByIdAsync(
                request.Id,
                cancellationToken,
                includeReference: "Course");

            if (message == null)
                throw new NotFoundException(nameof(Message), request.Id);
            if (message.Course == null)
                throw new NotFoundException(nameof(Course), request.Id);
            else if (request.CourseId != null && message.CourseId != request.CourseId)
                throw new NotContainsException(nameof(Course), request.CourseId.Value, nameof(Message), request.Id);
            else if (message.SenderGuid != request.SenderGuid)
                throw new NoAccessException(nameof(Message), request.Id);

            if (message.QuestionId == null)
            {
                var answers = await _messageRepository.GetAllAsync(
                    cancellationToken,
                    filter: a => a.QuestionId == message.Id);
                foreach (var answer in answers)
                {
                    await _messageRepository.DeleteAsync(answer, cancellationToken);
                }
            }

            await _messageRepository.DeleteAsync(message, cancellationToken);
        }
    }
}
