﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Application.Handlers.Lessons.Commands.DeleteLesson
{
    public class DeleteLessonCommandValidator : AbstractValidator<DeleteLessonCommand>
    {
        public DeleteLessonCommandValidator()
        {
            RuleFor(comm => comm.Id).GreaterThan(0);
            RuleFor(comm => comm.CourseId).GreaterThan(0);
            //RuleFor(comm => comm.CoachGuid).NotEmpty();
        }
    }
}