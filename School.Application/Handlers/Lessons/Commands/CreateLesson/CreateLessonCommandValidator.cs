using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Commands.CreateLesson
{
    public class CreateLessonCommandValidator : AbstractValidator<CreateLessonCommand>
    {
        public CreateLessonCommandValidator()
        {
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            //RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.Number).GreaterThan(0);
            RuleFor(comm => comm.Title).NotEmpty().MaximumLength(256);
        }
    }
}
