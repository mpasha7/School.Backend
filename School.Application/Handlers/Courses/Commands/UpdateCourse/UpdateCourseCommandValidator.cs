using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.UpdateCourse
{
    public class UpdateCourseCommandValidator : AbstractValidator<UpdateCourseCommand>
    {
        public UpdateCourseCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            //RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.Title).NotEmpty().MaximumLength(256);
            RuleFor(comm => comm.Description).NotEmpty();
        }
    }
}
