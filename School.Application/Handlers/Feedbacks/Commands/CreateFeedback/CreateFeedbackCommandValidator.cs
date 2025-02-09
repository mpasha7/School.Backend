using FluentValidation;

namespace School.Application.Handlers.Feedbacks.Commands.CreateFeedback
{
    public class CreateFeedbackCommandValidator : AbstractValidator<CreateFeedbackCommand>
    {
        public CreateFeedbackCommandValidator()
        {
            RuleFor(comm => comm.CoachGuid).NotEmpty();

            RuleFor(comm => comm.Text).NotEmpty();

            RuleFor(comm => comm.ReportId).GreaterThan(0);
            RuleFor(comm => comm.LessonId).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
