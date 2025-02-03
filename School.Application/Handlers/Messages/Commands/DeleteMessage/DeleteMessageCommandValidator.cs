using FluentValidation;

namespace School.Application.Handlers.Messages.Commands.DeleteMessage
{
    public class DeleteMessageCommandValidator : AbstractValidator<DeleteMessageCommand>
    {
        public DeleteMessageCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.SenderGuid).NotEmpty();
        }
    }
}
