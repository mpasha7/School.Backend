using FluentValidation;

namespace School.Application.Handlers.Courses.Queries.PublicCourseDetails
{
    public class PublicCourseDetailsQueryValidator : AbstractValidator<PublicCourseDetailsQuery>
    {
        public PublicCourseDetailsQueryValidator()
        {
            RuleFor(q => q.Id).GreaterThan(0);
        }
    }
}
