using FluentValidation;

namespace School.Application.Handlers.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
    {
        public DeleteLessonCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}
