﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Courses.Commands.CreateCourse
{
    public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseCommandValidator()
        {
            //RuleFor(comm => comm.CoachGuid).NotEmpty();
            RuleFor(comm => comm.Title).NotEmpty().MaximumLength(200);
            RuleFor(comm => comm.Description).NotEmpty();
        }
    }
}
