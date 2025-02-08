using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Applies.Commands.CreateApply
{
    public class CreateApplyCommandValidator : AbstractValidator<CreateApplyCommand>
    {
        public CreateApplyCommandValidator()
        {
            RuleFor(comm => comm.StudentGuid).NotEmpty();
            RuleFor(comm => comm.StudentName).NotEmpty();
            RuleFor(comm => comm.CourseId).GreaterThan(0);
        }
    }
}
