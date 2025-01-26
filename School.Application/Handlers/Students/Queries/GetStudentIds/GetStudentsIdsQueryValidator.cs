using FluentValidation;
using School.Application.Handlers.Students.Queries.GetStudentsIds;

namespace School.Application.Handlers.Students.Queries.GetStudentIds
{
    public class GetStudentsIdsQueryValidator : AbstractValidator<GetStudentsIdsQuery>
    {
        public GetStudentsIdsQueryValidator()
        {
            RuleFor(q => q.CoachGuid).NotEmpty();
        }
    }
}
