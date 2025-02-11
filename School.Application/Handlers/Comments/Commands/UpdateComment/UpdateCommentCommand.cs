using MediatR;

namespace School.Application.Handlers.Comments.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest
    {
        public int Id { get; set; }

        public string? CoachGuid { get; set; }

        public int CourseId { get; set; }
    }
}
