using MediatR;
using School.Application.Common.Exceptions;
using School.Application.Interfaces.Repository;
using School.Domain;

namespace School.Application.Handlers.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, int>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly ICourseRepository _courseRepository;

        public CreateMessageCommandHandler(IMessageRepository messageRepository, ICourseRepository courseRepository)
        {
            this._messageRepository = messageRepository;
            this._courseRepository = courseRepository;
        }

        public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Course? course = new Course();
            if (request.CourseId != null)
            {
                course = await _courseRepository.GetByIdAsync(
                request.CourseId.Value,
                cancellationToken);

                if (course == null)
                    throw new NotFoundException(nameof(Course), request.CourseId.Value);
            }
            if (string.IsNullOrEmpty(request.SenderName))
                throw new ArgumentNullException(request.SenderName);

            var message = new Message
            {
                SenderGuid = request.SenderGuid,
                SenderName = request.SenderName,
                RecipientGuid = string.IsNullOrEmpty(request.RecipientGuid) ? course.CoachGuid : request.RecipientGuid,

                CreatedAt = DateTime.Now,
                Theme = request.Theme,
                Text = request.Text,
                Email = request.Email,
                Phone = request.Phone,
                IsRead = false,
                SenredRole = request.SenrerRole,
                QuestionId = request.QuestionId == 0 ? null : request.QuestionId,

                CourseId = request.CourseId
            };



            await _messageRepository.AddAsync(message, cancellationToken);
            return message.Id;
        }
    }
}
