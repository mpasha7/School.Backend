namespace School.Application.Handlers.Comments.Queries.GetCommentList
{
    public class CommentListVm
    {
        public IList<CommentLookupDto> Comments { get; set; } = new List<CommentLookupDto>();
    }
}
