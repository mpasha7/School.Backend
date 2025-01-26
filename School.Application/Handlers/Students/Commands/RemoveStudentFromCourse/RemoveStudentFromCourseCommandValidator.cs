using FluentValidation;

namespace School.Application.Handlers.Students.Commands.RemoveStudentFromCourse
{
    public class RemoveStudentFromCourseCommandValidator : AbstractValidator<RemoveStudentFromCourseCommand>
    {
        public RemoveStudentFromCourseCommandValidator()
        {
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
