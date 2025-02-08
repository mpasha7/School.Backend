using FluentValidation;

namespace School.Application.Handlers.Reports.Queries.GetReportDetails
{
    public class GetReportDetailsQueryValidator : AbstractValidator<GetReportDetailsQuery>
    {
        public GetReportDetailsQueryValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.LessonId).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}
