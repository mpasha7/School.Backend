using FluentValidation;

namespace School.Application.Handlers.Applies.Commands.DeleteApply
{
    public class DeleteApplyCommandValidator : AbstractValidator<DeleteApplyCommand>
    {
        public DeleteApplyCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
