using FluentValidation;

namespace School.Application.Handlers.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryValidator : AbstractValidator<GetCourseDetailsQuery>
    {
        public GetCourseDetailsQueryValidator()
        {
            RuleFor(q => q.Id).GreaterThan(0);
            RuleFor(q => q.CoachGuid).NotEmpty();
        }
    }
}
