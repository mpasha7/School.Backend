using FluentValidation;

namespace School.Application.Handlers.Students.Queries.GetStudentsIds
{
    public class GetStudentsIdsQueryValidator : AbstractValidator<GetStudentsIdsQuery>
    {
        public GetStudentsIdsQueryValidator()
        {
            RuleFor(q => q.CoachGuid).NotEmpty();
        }
    }
}
