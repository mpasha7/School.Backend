using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Applies.Commands.UpdateApply
{
    public class UpdateApplyCommandValidator : AbstractValidator<UpdateApplyCommand>
    {
        public UpdateApplyCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
