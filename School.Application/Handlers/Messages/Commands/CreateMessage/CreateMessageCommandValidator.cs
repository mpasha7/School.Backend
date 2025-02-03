using FluentValidation;

namespace School.Application.Handlers.Messages.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : AbstractValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator()
        {
            RuleFor(comm => comm.SenderGuid).NotEmpty();
            //RuleFor(comm => comm.RecipientGuid).NotEmpty();

            RuleFor(comm => comm.Text).NotEmpty();
        }
    }
}
