using MediatR;

namespace School.Application.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListQuery : IRequest<CommentListVm>
    {
        public int CourseId { get; set; }
        public string? CoachGuid { get; set; }
    }
}
