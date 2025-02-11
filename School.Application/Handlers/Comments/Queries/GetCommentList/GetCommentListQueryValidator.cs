using FluentValidation;

namespace School.Application.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryValidator : AbstractValidator<GetCommentListQuery>
    {
        public GetCommentListQueryValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}
