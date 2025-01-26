using FluentValidation;

namespace School.Application.Handlers.Students.Commands.AddStudentToCourse
{
    public class AddStudentToCourseCommandValidator : AbstractValidator<AddStudentToCourseCommand>
    {
        public AddStudentToCourseCommandValidator()
        {
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
