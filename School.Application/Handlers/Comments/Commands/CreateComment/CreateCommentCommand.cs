using MediatR;

namespace School.Application.Handlers.Comments.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<int>
    {
        public string StudentGuid { get; set; }
        public string StudentName { get; set; }

        public string Text { get; set; }

        public int CourseId { get; set; }
    }
}
