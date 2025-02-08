using FluentValidation;

namespace School.Application.Handlers.Reports.Queries.GetReportList
{
    public class GetReportListQueryValidator : AbstractValidator<GetReportListQuery>
    {
        public GetReportListQueryValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}
