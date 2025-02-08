using FluentValidation;

namespace School.Application.Handlers.Reports.Commands.CreateReport
{
    public class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.StudentName).NotEmpty();
            RuleFor(comm => comm.Text).NotEmpty();

            RuleFor(comm => comm.LessonId).GreaterThan(0);
        }
    }
}
