using FluentValidation;

namespace School.Application.Handlers.Comments.Commands.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.StudentName).NotEmpty();

            RuleFor(comm => comm.Text).NotEmpty().MaximumLength(2000);

            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
