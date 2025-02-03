using MediatR;
using School.Domain;

namespace School.Application.Handlers.Messages.Commands.CreateMessage
{
    public class CreateMessageCommand : IRequest<int>
    {
        public string SenderGuid { get; set; }
        public string SenderName { get; set; }
        public string RecipientGuid { get; set; }

        public string Theme { get; set; }
        public string Text { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public UserRoles SenrerRole { get; set; }
        public int? QuestionId { get; set; }

        public int? CourseId { get; set; }
    }
}
