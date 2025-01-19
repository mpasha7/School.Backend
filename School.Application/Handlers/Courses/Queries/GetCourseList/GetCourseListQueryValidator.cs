using FluentValidation;

namespace School.Application.Handlers.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryValidator : AbstractValidator<GetCourseListQuery>
    {
        public GetCourseListQueryValidator()
        {
            RuleFor(q => q.UserGuid).NotEmpty();
        }
    }
}
