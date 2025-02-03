using MediatR;

namespace School.Application.Handlers.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommand : IRequest
    {
        public int Id { get; set; }
        public int? CourseId { get; set; }
        public string SenderGuid {  get; set; }
    }
}
