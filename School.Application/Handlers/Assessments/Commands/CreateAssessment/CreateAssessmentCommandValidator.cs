using FluentValidation;

namespace School.Application.Handlers.Assessments.Commands.CreateAssessment
{
    public class CreateAssessmentCommandValidator : AbstractValidator<CreateAssessmentCommand>
    {
        public CreateAssessmentCommandValidator()
        {
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.Text).NotEmpty().MaximumLength(2000);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
