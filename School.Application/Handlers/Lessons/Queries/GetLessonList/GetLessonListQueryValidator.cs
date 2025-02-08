using FluentValidation;

namespace School.Application.Handlers.Lessons.Queries.GetLessonList
{
    public class GetLessonListQueryValidator : AbstractValidator<GetLessonListQuery>
    {
        public GetLessonListQueryValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.UserGuid).NotEmpty();
        }
    }
}
