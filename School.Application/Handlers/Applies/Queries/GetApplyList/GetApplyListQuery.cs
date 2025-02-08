using MediatR;

namespace School.Application.Handlers.Applies.Queries.GetApplyList
{
    public class GetApplyListQuery : IRequest<ApplyListVm>
    {
        public string? CoachGuid { get; set; }
        public int CourseId { get; set; }
    }
}
